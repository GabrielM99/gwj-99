public enum ComputerType{
    /// <summary>
    /// Existe por que a godot nao lida muito bem com export nullable
    /// </summary>
    None,
    /// <summary>
    /// Acesso as armas pra se defender
    /// </summary>
    Weapons,
    /// <summary>
    /// Comando dos robos
    /// </summary>
    Robots,
    /// <summary>
    /// Gerencia melhorias dos robos
    /// </summary>
    Upgrades,
    /// <summary>
    /// Permite ver o mapa e os robos
    /// </summary>
    Radar
}