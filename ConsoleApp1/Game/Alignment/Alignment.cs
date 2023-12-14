namespace ConsoleApp1.Game.Alignment;

/// <summary>
/// Представляет различные параметры выравнивания для макета или позиционирования.
/// </summary>
public enum Alignment
{
    /// <summary>
    /// Выравнивает предметы по началу контейнера.
    /// </summary>
    Start,

    /// <summary>
    /// Центрирует предметы в контейнере.
    /// </summary>
    Center,

    /// <summary>
    /// Выравнивает предметы до конца контейнера.
    /// </summary>
    End,

    /// <summary>
    /// Равномерно распределяет доступное пространство между предметами, не имея места до первого или после последнего элемента.
    /// </summary>
    SpaceBetween,

    /// <summary>
    /// Распределяет доступное пространство вокруг предметов с равным пространством с обеих сторон каждого предмета.
    /// </summary>
    SpaceAround,

    /// <summary>
    /// Равномерно распределяет доступное пространство вокруг предметов.
    /// </summary>
    SpaceEvenly
}