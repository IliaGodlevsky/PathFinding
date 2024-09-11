using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Shared.Primitives;
using Pathfinding.Shared.Random;
using Pathfinding.Shared.Extensions;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Infrastructure.Business.Test.TestRealizations;

namespace Pathfinding.Infrastructure.Business.Test
{
    internal static partial class EntityBuilder
    {
        private const string GraphName = "test";
        private const string AlgorithnName = "algorithm";
        private const int Limit = TestGraph.Width * TestGraph.Length / 10;

        private static readonly CongruentialRandom random = new(420);
        private static readonly InclusiveValueRange<int> range = new(TestGraph.Width - 1, 1);

        public static CreateAlgorithmRunHistoryRequest CreateAlgorithmRunHistoryRequest() => new();

        public static CreatePathfindingHistoryRequest<TestVertex> CreatePathfindingHistoryRequest() => new();

        public static PathfindingHistorySerializationModel CreatePathfindingHistorySerializationModel() => new();

        public static CreateSubAlgorithmRequest CreateSubAlgorithmRequest() => new();

        public static AlgorithmRunHistorySerializationModel CreateAlgorithmRunHistorySerializationModel() => new();

        public static GraphSerializationModel CreateGraphSerializationModelFull() => new()
        {
            DimensionSizes = TestGraph.Interface.DimensionsSizes,
            Name = GraphName,
            Vertices = TestMapper.Instance.Mapper
                    .Map<VertexSerializationModel[]>(TestGraph.Interface.ToArray())
        };


        public static CreateSubAlgorithmRequest WithOrder(this CreateSubAlgorithmRequest request, int order)
        {
            request.Order = order;
            return request;
        }

        public static CreateSubAlgorithmRequest WithRunId(this CreateSubAlgorithmRequest request, int runId)
        {
            request.AlgorithmRunId = runId;
            return request;
        }

        public static CreateSubAlgorithmRequest WithRandomVisited(this CreateSubAlgorithmRequest request, int count = 1)
        {
            var visited = new List<(Coordinate, IReadOnlyList<Coordinate>)>();
            while (count-- > 0)
            {
                var visitedCoordinate = random.GenerateCoordinate(range, 2);
                var enqueued = random.GenerateCoordinates(range, 2, Limit).ToList();
                visited.Add((visitedCoordinate, enqueued));
            }
            request.Visited = visited;
            return request;
        }

        public static CreateSubAlgorithmRequest WithRandomPath(this CreateSubAlgorithmRequest request)
        {
            request.Path = random.GenerateCoordinates(range, 2, Limit).ToList();
            return request;
        }

        public static PathfindingHistorySerializationModel WithTestGraph(this PathfindingHistorySerializationModel model)
        {
            var mapper = TestMapper.Instance.Mapper;
            var vertices = TestGraph.Instance.ToArray();
            model.Graph = new()
            {
                Name = GraphName,
                DimensionSizes = TestGraph.Instance.DimensionsSizes,
                Vertices = mapper.Map<List<VertexSerializationModel>>(vertices)
            };
            return model;
        }

        public static AlgorithmRunHistorySerializationModel WithAlgorithmRun(this AlgorithmRunHistorySerializationModel model)
        {
            model.Run = new()
            {
                AlgorithmId = AlgorithnName
            };
            return model;
        }

        public static AlgorithmRunHistorySerializationModel WithRandomGraphState(this AlgorithmRunHistorySerializationModel model)
        {
            model.GraphState = new()
            {
                Obstacles = GenerateCoordinateModels(),
                Range = GenerateCoordinateModels(),
                Costs = random.GenerateCoordinates((9, 1), 2, 25)
                    .Zip(random.GenerateNumbers(range, 25),
                    (x, y) => new CostCoordinatePair
                    {
                        Position = new CoordinateModel() { Coordinate = x.CoordinatesValues.ToArray() },
                        Cost = y
                    })
                    .ToList()
            };
            return model;
        }

        public static AlgorithmRunHistorySerializationModel WithRandomStatistics(this AlgorithmRunHistorySerializationModel model)
        {
            model.Statistics = new()
            {
                AlgorithmRunId = random.NextInt(10, 1),
                AlgorithmSpeed = TimeSpan.FromMilliseconds(random.NextDouble((5, 1))),
                Spread = random.NextInt((5, 1)),
                Cost = random.NextDouble((1500, 250)),
                Steps = random.NextInt((145, 20)),
                StepRule = "Euclidian",
                Heuristics = "Default",
                Elapsed = TimeSpan.FromMilliseconds(random.NextDouble((5, 1))),
                ResultStatus = "Success",
                Visited = random.NextInt((1000, 50))
            };
            return model;
        }

        public static AlgorithmRunHistorySerializationModel WithRandomSubAlgorithm(this AlgorithmRunHistorySerializationModel model, int count = 1)
        {
            var subAlgorithms = new List<SubAlgorithmSerializationModel>();
            while (count-- > 0)
            {
                var subAlgorithm = new SubAlgorithmSerializationModel()
                {
                    Order = count,
                    Visited = new List<VisitedVerticesModel>()
                    {
                        new() { Current = GenerateCoordinateModel(), Enqueued = GenerateCoordinateModels() },
                        new() { Current = GenerateCoordinateModel(), Enqueued = GenerateCoordinateModels() }
                    },
                    Path = GenerateCoordinateModels()
                };
                subAlgorithms.Add(subAlgorithm);
            }
            model.SubAlgorithms = subAlgorithms;
            return model;
        }

        public static PathfindingHistorySerializationModel WithRandomRange(this PathfindingHistorySerializationModel model)
        {
            model.Range = GenerateCoordinateModels();
            return model;
        }

        public static PathfindingHistorySerializationModel WithRandomRunHistory(this PathfindingHistorySerializationModel model, int count = 1)
        {
            var algorithms = new List<AlgorithmRunHistorySerializationModel>();
            while (count-- > 0)
            {
                var runHistory = CreateAlgorithmRunHistorySerializationModel()
                    .WithAlgorithmRun()
                    .WithRandomGraphState()
                    .WithRandomStatistics()
                    .WithRandomSubAlgorithm(2);
                algorithms.Add(runHistory);
            }
            model.Algorithms = algorithms;
            return model;
        }

        public static CreateAlgorithmRunHistoryRequest WithRandomRun(this CreateAlgorithmRunHistoryRequest request)
        {
            request.Run = new CreateAlgorithmRunRequest()
            {
                GraphId = random.NextInt(10, 1),
                AlgorithmId = AlgorithnName
            };
            return request;
        }

        public static CreateAlgorithmRunHistoryRequest WithRandomSubAlgorithm(this CreateAlgorithmRunHistoryRequest request, int count = 1)
        {
            var subAlgorithms = new List<CreateSubAlgorithmRequest>();
            while (count-- > 0)
            {
                var createSubAlgorithmRequest = CreateSubAlgorithmRequest()
                    .WithRandomPath()
                    .WithRandomVisited(2)
                    .WithOrder(count);
                subAlgorithms.Add(createSubAlgorithmRequest);
            }
            request.SubAlgorithms = subAlgorithms;
            return request;
        }

        public static CreateAlgorithmRunHistoryRequest WithRandomGraphState(this CreateAlgorithmRunHistoryRequest request)
        {
            request.GraphState = new()
            {
                Costs = random.GenerateCoordinates((9, 1), 2, 25)
                    .Zip(random.GenerateNumbers(range, 25),
                    (x, y) => (Position : x, Cost: y))
                    .ToList(),
                Regulars = random.GenerateCoordinates(range, 2, Limit).ToList(),
                Obstacles = random.GenerateCoordinates(range, 2, Limit).ToList(),
                Range = random.GenerateCoordinates(range, 2, Limit).ToList()
            };
            return request;
        }

        public static CreateAlgorithmRunHistoryRequest WithRandomStatisitics(this CreateAlgorithmRunHistoryRequest request)
        {
            request.Statistics = new()
            {
                AlgorithmId = AlgorithnName,
                Spread = random.NextInt((5, 1)),
                Cost = random.NextDouble((1500, 250)),
                Steps = random.NextInt((145, 20)),
                StepRule = "Euclidian",
                Heuristics = "Default",
                Elapsed = TimeSpan.FromMilliseconds(random.NextDouble((5, 1))),
                ResultStatus = "Success",
                Visited = random.NextInt((1000, 50))
            };
            return request;
        }

        public static CreatePathfindingHistoryRequest<TestVertex> WithTestGraph(this CreatePathfindingHistoryRequest<TestVertex> request)
        {
            request.Graph = new CreateGraphRequest<TestVertex>()
            {
                Graph = TestGraph.Interface,
                Name = GraphName,
            };
            return request;
        }

        public static CreatePathfindingHistoryRequest<TestVertex> WithRandomRunHistory(this CreatePathfindingHistoryRequest<TestVertex> request, int count = 1)
        {
            var runHistories = new List<CreateAlgorithmRunHistoryRequest>();
            while (count-- > 0)
            {
                var runHistory = 
                    CreateAlgorithmRunHistoryRequest()
                    .WithRandomRun()
                    .WithRandomGraphState()
                    .WithRandomStatisitics()
                    .WithRandomSubAlgorithm(2);
                runHistories.Add(runHistory);
            }
            request.Algorithms = runHistories;
            return request;
        }

        public static CreatePathfindingHistoryRequest<TestVertex> WithRandomRange(this CreatePathfindingHistoryRequest<TestVertex> request)
        {
            request.Range = random.GenerateCoordinates(range, 2, random.NextInt(Limit, 5)).ToList();
            return request;
        }

        private static List<CoordinateModel> GenerateCoordinateModels()
        {
            return random.GenerateCoordinates(range, 2, Limit)
                    .Select(x => new CoordinateModel() { Coordinate = x.CoordinatesValues })
                    .ToList();
        }

        private static CoordinateModel GenerateCoordinateModel()
        {
            return new CoordinateModel()
            {
                Coordinate = random.GenerateCoordinate(range, 2).CoordinatesValues
            };
        }
    }
}
