// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace WikidataGame.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Game
    {
        /// <summary>
        /// Initializes a new instance of the Game class.
        /// </summary>
        public Game()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the Game class.
        /// </summary>
        public Game(System.Guid? id = default(System.Guid?), IList<IList<Tile>> tiles = default(IList<IList<Tile>>), IList<System.Guid?> winningPlayerIds = default(IList<System.Guid?>), Player me = default(Player), Player opponent = default(Player), System.Guid? nextMovePlayerId = default(System.Guid?), System.DateTime? moveExpiry = default(System.DateTime?), bool? awaitingOpponentToJoin = default(bool?))
        {
            Id = id;
            Tiles = tiles;
            WinningPlayerIds = winningPlayerIds;
            Me = me;
            Opponent = opponent;
            NextMovePlayerId = nextMovePlayerId;
            MoveExpiry = moveExpiry;
            AwaitingOpponentToJoin = awaitingOpponentToJoin;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public System.Guid? Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "tiles")]
        public IList<IList<Tile>> Tiles { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "winningPlayerIds")]
        public IList<System.Guid?> WinningPlayerIds { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "me")]
        public Player Me { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "opponent")]
        public Player Opponent { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "nextMovePlayerId")]
        public System.Guid? NextMovePlayerId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "moveExpiry")]
        public System.DateTime? MoveExpiry { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "awaitingOpponentToJoin")]
        public bool? AwaitingOpponentToJoin { get; set; }

    }
}
