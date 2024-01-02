﻿using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Interface
{
    internal interface IService
    {
        IGraph<Vertex> GetGraph(int id);

        IReadOnlyCollection<int> GetGraphIds();

        IReadOnlyList<GraphEntity> GetAllGraphInfo();

        IReadOnlyCollection<AlgorithmReadDto> GetGraphPathfindingHistory(int graphId);

        PathfindingHistoryReadDto GetPathfindingHistory(int graphId);

        IReadOnlyCollection<ICoordinate> GetRange(int graphId);

        bool UpdateObstaclesCount(int newCount, int graphId);

        PathfindingHistoryReadDto AddPathfindingHistory(PathfindingHistoryCreateDto history);

        PathfindingHistoryReadDto AddPathfindingHistory(PathfindingHistorySerializationDto history);

        PathfindingHistorySerializationDto GetSerializationHistory(int graphId);

        GraphSerializationDto GetSerializationGraph(int graphId);

        int AddGraph(GraphSerializationDto graph);

        int AddGraph(IGraph<Vertex> graph);

        int AddAlgorithm(AlgorithmCreateDto algorithm);

        bool AddNeighbors(IReadOnlyDictionary<int, int[]> neighborhoods);

        bool UpdateVertices(IEnumerable<Vertex> vertices, int graphId);

        bool AddRange((int Order, Vertex Vertex)[] vertices, int graphId);

        bool RemoveNeighbors(IReadOnlyDictionary<int, int[]> neighborhoods);

        bool UpdateRange((int Order, Vertex Vertex)[] vertices, int graphId);

        bool RemoveRange(IEnumerable<Vertex> vertices, int graphId);

        bool RemoveRange(int graphId);

        bool DeleteGraph(int graphId);
    }
}