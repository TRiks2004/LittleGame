namespace MiniGame.Items.Display;

/// <summary>
/// Enumeration representing various mapping scenarios or states.
/// Each member corresponds to a specific mapping type with an assigned integer value.
/// </summary>
public enum Mapping
{
    /// <summary>
    /// Пустота : 0.
    /// </summary>
    Void = 0,

    /// <summary>
    /// Пустота квадрата : 101
    /// </summary>
    VoidSquare = 101,

    /// <summary>
    /// Верхний правый угол : 57
    /// </summary>
    UpperRightCorner = 57,

    /// <summary>
    /// Верхний левый угол : 56
    /// </summary>
    UpperLeftCorner = 56,

    /// <summary>
    /// Нижний правый угол : 59
    /// </summary>
    LowerRightCorner = 59,

    /// <summary>
    /// Нижний левый угол : 58
    /// </summary>
    LowerLeftCorner = 58,

    /// <summary>
    /// Вертикальная линия : 60
    /// </summary>
    VerticalLine = 60,

    /// <summary>
    /// Горизонтальная линия : 61
    /// </summary>
    HorizontalLine = 61
}
