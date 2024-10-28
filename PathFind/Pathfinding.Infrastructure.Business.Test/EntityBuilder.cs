using Pathfinding.Infrastructure.Business.Test.TestRealizations;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using Pathfinding.Shared.Random;

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

        public static AlgorithmRunHistorySerializationModel CreateAlgorithmRunHistorySerializationModel() => new();

        public static GraphSerializationModel CreateGraphSerializationModelFull() => new()
        {
            DimensionSizes = TestGraph.Interface.DimensionsSizes,
            Name = GraphName,
            SmoothLevel = "Test",
            Neighborhood = "Test",
            Vertices = TestMapper.Instance.Mapper
                    .Map<VertexSerializationModel[]>(TestGraph.Interface.ToArray())
        };

        public static PathfindingHistorySerializationModel WithTestGraph(this PathfindingHistorySerializationModel model)
        {
            var mapper = TestMapper.Instance.Mapper;
            var vertices = TestGraph.Instance.ToArray();
            model.Graph = new()
            {
                Name = GraphName,
                Neighborhood = "Test",
                SmoothLevel = "Test",
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

        public static AlgorithmRunHistorySerializationModel WithRandomStatistics(this AlgorithmRunHistorySerializationModel model)
        {
            model.Statistics = new()
            {
                AlgorithmRunId = random.NextInt(10, 1),
                Weight = 0,
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
                    .WithRandomStatistics();
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

        public static CreateAlgorithmRunHistoryRequest WithRandomStatisitics(this CreateAlgorithmRunHistoryRequest request)
        {
            request.Statistics = new()
            {
                AlgorithmId = AlgorithnName,
                Weight = 0,
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
                SmoothLevel = "Test",
                Neighborhood = "Test"
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
                    .WithRandomStatisitics();
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
