﻿using LiteDB;
using Pathfinding.App.Console.DAL.Attributes;
using Pathfinding.App.Console.DAL.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pathfinding.App.Console.DAL.Models.Entities
{
    [Table(DbTables.Algorithms)]
    [BsonTable(DbTables.Algorithms)]
    internal class AlgorithmEntity : IEntity
    {
        [BsonId]
        [NotNull]
        [Identity(true)]
        public int Id { get; set; }

        [NotNull]
        [Index]
        [Reference(DbTables.Graphs, nameof(GraphEntity.Id), OnDelete.Cascade)]
        public int GraphId { get; set; }

        public string Statistics { get; set; }

        public byte[] Path { get; set; }

        public byte[] Obstacles { get; set; }

        public byte[] Visited { get; set; }

        public byte[] Range { get; set; }

        public byte[] Costs { get; set; }
    }
}
