Console.OutputEncoding = System.Text.Encoding.Default;

#region Constants
const int BLACK = 0;
const int BROWN = 1;
const int RED = 2;
const int ORANGE = 3;
const int YELLOW = 4;
const int GREEN = 5;
const int BLUE = 6;
const int VIOLET = 7;
const int GRAY = 8;
const int WHITE = 9;
const int GOLD = 10;
const int SILVER = 11;

const string VALUE = "value";
const string MULTIPLIER = "multiplier";
const string TOLERANCE = "tolerance";
#endregion

#region Main Program
{
    string[] colorBands = args[0].Split('-');
    if (!(colorBands.Length is 4 or 5)) { Console.WriteLine("Invalid input length. Please provide 4 or 5 color bands."); return; }

    double resistance = 0, tolerance = 0;
    for (int i = 0; i < colorBands.Length; i++)
    {
        try
        {
            if (((i is 0 or 1) || (colorBands.Length == 5 && i == 2))
                && TryGetFromColorBand(colorBands[i], VALUE, out double resistanceValue))
            {
                resistance *= 10;
                resistance += resistanceValue;
            }

            else if (i == colorBands.Length - 1
                    && TryGetFromColorBand(colorBands[i], TOLERANCE, out double toleranceValue))
            {
                tolerance = toleranceValue;
            }

            else if (i == colorBands.Length - 2
                    && TryGetFromColorBand(colorBands[i], MULTIPLIER, out double multiplierValue))
            {
                resistance *= multiplierValue;
            }

            else { throw new ArgumentException(colorBands[i].Length == 3 ? "Invalid color code. Please use valid color codes." : "Invalid input format. Please use a hyphen (-) to separate color codes."); }
        }
        catch (ArgumentException e) { Console.WriteLine(e.Message); return; }
    }

    Console.WriteLine($"Resistance: {Math.Round(resistance, 2).ToString("N2")} Ω");
    Console.WriteLine($"Tolerance: ± {tolerance} %");
}
#endregion

#region Methods
double GetMultiplier(int color) => Math.Pow(10, color);

bool TryGetFromColorBand(string color, string item, out double value)
{
    var resistors = new Dictionary<string, double?>[]
    {
        new Dictionary<string, double?>
        {
            {VALUE, BLACK},
            {MULTIPLIER, GetMultiplier(BLACK)},
            {TOLERANCE, null}
        },
        new Dictionary<string, double?>
        {
            {VALUE, BROWN},
            {MULTIPLIER, GetMultiplier(BROWN)},
            {TOLERANCE, 1.0}
        },
        new Dictionary<string, double?>
        {
            {VALUE, RED},
            {MULTIPLIER, GetMultiplier(RED)},
            {TOLERANCE, 2.0}
        },
        new Dictionary<string, double?>
        {
            {VALUE, ORANGE},
            {MULTIPLIER, GetMultiplier(ORANGE)},
            {TOLERANCE, null}
        },
        new Dictionary<string, double?>
        {
            {VALUE, YELLOW},
            {MULTIPLIER, GetMultiplier(YELLOW)},
            {TOLERANCE, null}
        },
        new Dictionary<string, double?>
        {
            {VALUE, GREEN},
            {MULTIPLIER, GetMultiplier(GREEN)},
            {TOLERANCE, 0.5}
        },
        new Dictionary<string, double?>
        {
            {VALUE, BLUE},
            {MULTIPLIER, GetMultiplier(BLUE)},
            {TOLERANCE, 0.25}
        },
        new Dictionary<string, double?>
        {
            {VALUE, VIOLET},
            {MULTIPLIER, GetMultiplier(VIOLET)},
            {TOLERANCE, 0.1}
        },
        new Dictionary<string, double?>
        {
            {VALUE, GRAY},
            {MULTIPLIER, GetMultiplier(GRAY)},
            {TOLERANCE, 0.05}
        },
        new Dictionary<string, double?>
        {
            {VALUE, WHITE},
            {MULTIPLIER, GetMultiplier(WHITE)},
            {TOLERANCE, null}
        },
        new Dictionary<string, double?>
        {
            {VALUE, null},
            {MULTIPLIER, 0.1},
            {TOLERANCE, 5.0}
        },
        new Dictionary<string, double?>
        {
            {VALUE, null},
            {MULTIPLIER, 0.01},
            {TOLERANCE, 10.0}
        }
    };

    int arrayItem = color switch
    {
        "Bla" => BLACK,
        "Bro" => BROWN,
        "Red" => RED,
        "Ora" => ORANGE,
        "Yel" => YELLOW,
        "Gre" => GREEN,
        "Blu" => BLUE,
        "Vio" => VIOLET,
        "Gra" => GRAY,
        "Whi" => WHITE,
        "Gol" => GOLD,
        "Sil" => SILVER,
        _ => -1
    };

    value = 0;
    if (arrayItem == -1) { return false; }
    else
    {
        double? output = resistors[arrayItem][item];

        if (output == null) { return false; }
        else { value = (double)output; return true; }
    }
}
#endregion
