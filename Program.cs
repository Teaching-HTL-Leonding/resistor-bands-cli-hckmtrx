Console.OutputEncoding = System.Text.Encoding.Default;

#region Constants
const string BLACK = "Bla";
const string BROWN = "Bro";
const string RED = "Red";
const string ORANGE = "Ora";
const string YELLOW = "Yel";
const string GREEN = "Gre";
const string BLUE = "Blu";
const string VIOLET = "Vio";
const string GRAY = "Gra";
const string WHITE = "Whi";
const string GOLD = "Gol";
const string SILVER = "Sil";

const string DIGIT = "digit";
const string MULTIPLIER = "multiplier";
const string TOLERANCE = "tolerance";
#endregion

#region Main Program
{
    string[] colorBands = args[0].Split('-');
    if (!(colorBands.Length is 4 or 5))
    {
        Console.WriteLine("Invalid input length. Please provide 4 or 5 color bands.");
        return;
    }

    double resistance = 0, tolerance = 0;
    for (int i = 0; i < colorBands.Length; i++)
    {
        try
        {
            if (((i is 0 or 1) || (colorBands.Length == 5 && i == 2))
                && TryGetFromColorBand(colorBands[i], DIGIT, out double digitValue))
            {
                resistance *= 10;
                resistance += digitValue;
            }

            else if (i == colorBands.Length - 2
                    && TryGetFromColorBand(colorBands[i], MULTIPLIER, out double multiplierValue))
            {
                resistance *= multiplierValue;
            }

            else if (i == colorBands.Length - 1
                    && TryGetFromColorBand(colorBands[i], TOLERANCE, out double toleranceValue))
            {
                tolerance = toleranceValue;
            }

            else { throw new ArgumentException(colorBands[i].Length == 3 ? "Invalid color code. Please use valid color codes." : "Invalid input format. Please use a hyphen (-) to separate color codes."); }
        }
        catch (ArgumentException e) { Console.WriteLine(e.Message); return; }
    }

    Console.WriteLine($"Resistance: {Math.Round(resistance, 2).ToString("N2")} Ω");
    Console.WriteLine($"Tolerance: ± {tolerance}%");
}
#endregion

#region Methods
bool TryGetFromColorBand(string color, string item, out double value)
{
    value = 0;
    string[] colors = { BLACK, BROWN, RED, ORANGE, YELLOW, GREEN, BLUE, VIOLET, GRAY, WHITE };

    int arrayIndex = Array.IndexOf(colors, color);

    if (arrayIndex == -1)
    {
        if (color is GOLD or SILVER && item != DIGIT)
        {
            value = color switch
            {
                GOLD => item == MULTIPLIER ? 0.1 : 5,
                SILVER => item == MULTIPLIER ? 0.01 : 10,
                _ => value
            };

            return true;
        }

        return false;
    }
    else
    {
        if (item == TOLERANCE)
        {
            if (color is BLACK or ORANGE or YELLOW or WHITE) { return false; }

            value = color switch
            {
                BROWN => 1,
                RED => 2,
                GREEN => 0.5,
                BLUE => 0.25,
                VIOLET => 0.1,
                GRAY => 0.05,
                _ => value
            };
        }
        else
        {
            value = item switch
            {
                DIGIT => arrayIndex,
                MULTIPLIER => Math.Pow(10, arrayIndex),
                _ => value
            };
        }

        return true;
    }
}
#endregion
