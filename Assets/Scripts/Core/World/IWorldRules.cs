namespace TWF
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Root interface to define the rules of the world.
    ///
    /// This includes:
    /// - the resources of the world, as well as their traits (defined as components)
    /// - the terrains of the world, as well as their traits (defined as components)
    /// - the zones of the world, as well as their traits (defined as components)
    /// - the buildings models of the world (defined as components)
    /// - the traits of the buildings of the world (defined as components)
    /// - the background agents that mutate the state of the world
    /// - the tools that the player is allowed to use
    ///
    /// About buildings:
    ///
    /// World buildings are defined in a registry of Entities.
    /// The building map type allows to perform geographical lookups of location to building id.
    /// Each building is related to a particular model, this is a Component of the building Entities.
    /// Other Components can be attached to buildings, such as a grahpical variant which corresponds to a different graphical rendering.
    /// The effect of the building on the gameplay is however completely defined in the building model.
    /// Building model itself is another registry Entities to which components can be attached to, to provide specific behavior.
    ///
    /// Let's provide an example which ties everything together.
    ///
    /// The map at location (x, y) will reference the building B, which is a number between 0 and "number of buildings in the current world".
    /// B has a model BM, a number corresponding to the building model "house".
    /// B has also a graphical component BG, a number corresponding to the variant "yellow house with red roof"
    /// BM itself has many components that define the effect of a house on the gameplay, such as its population, its elecriticy consomption, etc.
    /// </summary>
    public interface IWorldRules
    {
        IReadOnlyNamedEntities Resources { get; }

        IReadOnlyNamedEntities Terrains { get; }

        IReadOnlyNamedEntities Zones { get; }

        IReadOnlyNamedEntities BuildingModels { get; }

        IReadOnlyDictionary<string, IReadOnlyComponents> BuildingComponents { get; }

        IReadOnlyDictionary<string, ScheduledAgent> Agents { get; }

        IReadOnlyDictionary<string, IModifiableToolBehavior> ToolBehaviors { get; }

        IReadOnlyDictionary<string, IToolBrush> ToolBrushes { get; }

        Random Random { get; }

        OnNewWorldListener OnNewWorldListener { get; }

        IConfigProvider ConfigProvider { get; }
    }
}
