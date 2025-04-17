using System;
class MyProgram()
{
    //--CUSTOMIZABLE VARIABLES--
    static class Custom
    {
        //IF SOMETHING DOESN'T WORK, IT'S YOUR FAULT.. PROBABLY
        static public readonly char bracketOne = '-';
        static public readonly char bracketTwo = ':';
        static public readonly char bracketThree = '+';
        static public readonly char bracketFour = ' ';
        static public readonly char bracketFive = '?';
        static public readonly char bracketSix = '|';
        static public readonly char bracketSeven = 'X';
        static public readonly char bracketEight = '!';
        static public readonly char bracketNine = 'J';
        static public readonly char bracketTen = 'j';
        static public readonly char bracketEleven = 'I';
        static public readonly char bracketTwelve = '+';
        static public readonly int inventoryX = 20;
        static public readonly int inventoryY = 8;
        static public readonly int inventorySpaceX = 3;
        static public readonly int inventorySpaceY = 1;
        static public int spaceX = 10;
        static public int spaceY = 6;
        static int test = 0;
    }

    //--NON-CUSTOMIZABLE VARIABLES--
    static class Universal
    {
        static public bool placing = false;
        static public bool[,] placer = new bool[Custom.inventoryY + Custom.spaceY * 2, Custom.inventoryX + Custom.spaceX * 2];

        static public bool[,] selector = new bool[Custom.inventoryY, Custom.inventoryX];

        static public bool[,] animPos = new bool[Custom.inventoryY + Custom.spaceY * 2, Custom.inventoryX + Custom.spaceX * 2];

        public class ImprintData
        {
            public readonly int itemNumber;
            public readonly int listPos;
            public int stackAmount;
            static public int[,] imprintArray = new int[Custom.inventoryY, Custom.inventoryX];
            public static List<ImprintData> imprintList = new List<ImprintData>();
            public ImprintData(int itemNumber, int stackAmount)
            {
                this.itemNumber = itemNumber;
                this.stackAmount = stackAmount;
                this.listPos = imprintList.Count;
                imprintList.Add(this);
            }

        }

        public class Item
        {
            public readonly string name;
            public readonly string desc;
            public readonly int maxStack;
            public readonly char invChar;
            public readonly ConsoleColor invColor;
            public readonly bool[,] spaces;
            private static List<Item> items { get; } = new List<Item>();
            public static IReadOnlyList<Item> itemList => items.AsReadOnly();
            public Item(string name, string desc, int maxStack, char invChar, ConsoleColor invColor, bool[,] spaces)
            {
                this.name = name;
                this.desc = desc;
                this.maxStack = maxStack;
                this.invChar = invChar;
                this.invColor = invColor;
                this.spaces = spaces;

                items.Add(this);
            }
        }

        static public bool error = false;
        static public bool stack = false;

        static readonly public int consoleWidth = (Custom.spaceX * 2 + Custom.inventoryX) * (Custom.inventorySpaceX + 1) + 1;
        static readonly public int consoleHeight = (Custom.spaceY * 2 + Custom.inventoryY) * (Custom.inventorySpaceY + 1) + 1;
        static readonly public int placerWidth = Custom.spaceX * 2 + Custom.inventoryX;
        static readonly public int placerHeight = Custom.spaceY * 2 + Custom.inventoryY;

        static readonly public int invConLeft = Custom.spaceX * (Custom.inventorySpaceX + 1);
        static readonly public int invConRight = (Custom.spaceX + Custom.inventoryX) * (Custom.inventorySpaceX + 1);
        static readonly public int invConTop = Custom.spaceY * (Custom.inventorySpaceY + 1);
        static readonly public int invConBottom = (Custom.spaceY + Custom.inventoryY) * (Custom.inventorySpaceY + 1);
    }

    //--SET STUFF UP--
    static void Main()
    {
        SetConsoleSize();
        InitializeItems();
        InitializeArrays();
        DrawInventory();

        UserMain();
    }

    //--SET CONSOLE SIZE--
    static void SetConsoleSize()
    {
        Console.SetWindowSize(Universal.consoleWidth, Universal.consoleHeight);
    }

    //--INITIALIZE INVENTORY ITEMS--
    static void InitializeItems()
    {
        bool[,] hatchetSpaces =
        {
            {true, true, true, true, true },
            {false, false, false, true, true }
        };
        Universal.Item hatchetObj = new Universal.Item("Hatchet", "bonk", 1, 'Y', ConsoleColor.Blue, hatchetSpaces);

        bool[,] daggerSpaces =
        {
            {true, true, true }
        };
        Universal.Item daggerObj = new Universal.Item("Dagger", "shank", 1, 'R', ConsoleColor.Green, daggerSpaces);

        bool[,] greatswordSpaces =
        {
            {false, false, true, false, false, false, false, false, false, false },
            {true, true, true, true, true, true, true, true, true, true },
            {false, false, true, false, false, false, false, false, false, false }
        };
        Universal.Item greatswordObj = new Universal.Item("Greatsword", "big bonk", 1, 'S', ConsoleColor.DarkGray, greatswordSpaces);

        bool[,] greatGreatswordSpaces =
        {
            {false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, false,},
            {true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
            {false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, false }
        };
        Universal.Item greatGreatswordObj = new Universal.Item("Great Greatsword", "It was too big to be called a sword. Massive, thick, heavy, and far too rough. Indeed, it was a heap of raw iron.", 1, 'H', ConsoleColor.DarkRed, greatGreatswordSpaces);

        bool[,] warhammerSpaces =
        {
            {false, false, false, false, false, false, false, false, false, false, true},
            {false, false, false, false, false, false, false, false, false, true, true },
            {true, true, true, true, true, true, true, true, true, true, true },
            {false, false, false, false, false, false, false, false, false, true, true }
        };
        Universal.Item warhammerObj = new Universal.Item("Warhammer", "smash", 40000, 'W', ConsoleColor.Red, warhammerSpaces);

        bool[,] coinSpaces =
        {
            {true}
        };
        Universal.Item coinObj = new Universal.Item("Coin", "bling bling", 10, 'M', ConsoleColor.DarkBlue, coinSpaces);

        bool[,] arrowSpaces =
        {
            {true, true, true, true, true }
        };
        Universal.Item arrowObj = new Universal.Item("Arrow", "pew pew", 5, 'P', ConsoleColor.Gray, arrowSpaces);

        bool[,] bowSpaces =
        {
            {false, false, true, true, false, false },
            {false, true, false, false, true, false },
            {true, true, true, true, true, true }
        };
        Universal.Item bowObj = new Universal.Item("Bow", "twang", 1, 'B', ConsoleColor.DarkMagenta, bowSpaces);

        bool[,] holyHandGrenadeSpaces =
        {
            {true, true},
            {true, true}
        };
        Universal.Item holyHandGrenadeObj = new Universal.Item("Holy Hand Grenade", "bless this mess", 3, 'G', ConsoleColor.DarkYellow, holyHandGrenadeSpaces);
    }

    //--INITIALIZE UNIVERSALLY USED ARRAYS--
    static void InitializeArrays()
    {
        for (int i = 0; i < Custom.inventoryY; i++)
        {
            for (int j = 0; j < Custom.inventoryX; j++)
            {
                Universal.selector[i, j] = false;
                Universal.ImprintData.imprintArray[i, j] = -1;
            }
        }
        for (int i = 0; i < Universal.placerHeight; i++)
        {
            for (int j = 0; j < Universal.placerWidth; j++)
            {
                Universal.placer[i, j] = false;
            }
        }
    }

    //--DRAW THE INVENTORY FRAME WITHOUT THE TEXT AT THE BOTTOM--
    static void DrawInventory()
    {
        for (int i = 0; i <= Custom.spaceY * 2 + Custom.inventoryY; i++)
        {
            for (int j = 0; j <= Custom.inventorySpaceY; j++)
            {
                if (j == 0)
                {
                    for (int k = 0; k <= Universal.placerWidth; k++)
                    {
                        for (int l = 0; l <= Custom.inventorySpaceX; l++)
                        {
                            if (l == 0)
                            {
                                Console.SetCursorPosition(k * (Custom.inventorySpaceX + 1), i * (Custom.inventorySpaceY + 1) + j);
                                Console.Write(Custom.bracketSix); //OUTSIDE CONSOLE INTERSECT
                            }
                            else if (k != Universal.placerWidth)
                            {
                                Console.SetCursorPosition(k * (Custom.inventorySpaceX + 1) + l, i * (Custom.inventorySpaceY + 1) + j);
                                Console.Write(Custom.bracketFour); //OUTSIDE CONSOLE X BRACKET
                            }
                        }
                    }
                }
                else if (i != Custom.spaceY * 2 + Custom.inventoryY)
                {
                    for (int k = 0; k <= Universal.placerWidth; k++)
                    {
                        for (int l = 0; l <= Custom.inventorySpaceX; l++)
                        {
                            if (l == 0)
                            {
                                Console.SetCursorPosition(k * (Custom.inventorySpaceX + 1), i * (Custom.inventorySpaceY + 1) + j);
                                Console.Write(Custom.bracketFour); //OUTSIDE CONSOLE Y BRACKET
                            }
                            else if (k != Universal.placerWidth)
                            {
                                Console.SetCursorPosition(k * (Custom.inventorySpaceX + 1) + l, i * (Custom.inventorySpaceY + 1) + j);
                                Console.Write(Custom.bracketFour); //OUTSIDE CONSOLE EMPTY BRACKET
                            }
                        }
                    }
                }
            }
        }
        for (int i = Custom.spaceY; i <= Custom.spaceY + Custom.inventoryY; i++)
        {
            for (int j = 0; j <= Custom.inventorySpaceY; j++)
            {
                if (j == 0)
                {
                    for (int k = Custom.spaceX; k <= Custom.spaceX + Custom.inventoryX; k++)
                    {
                        for (int l = 0; l <= Custom.inventorySpaceX; l++)
                        {
                            if (l == 0)
                            {
                                Console.SetCursorPosition(k * (Custom.inventorySpaceX + 1), i * (Custom.inventorySpaceY + 1) + j);
                                Console.Write(Custom.bracketThree); //INSIDE CONSOLE INTERSECT BRACKET
                            }
                            else if (k != Custom.spaceX + Custom.inventoryX)
                            {
                                Console.SetCursorPosition(k * (Custom.inventorySpaceX + 1) + l, i * (Custom.inventorySpaceY + 1) + j);
                                Console.Write(Custom.bracketOne); //INSIDE CONSOLE X BRACKET
                            }
                        }
                    }
                }
                else if (i != Custom.spaceY + Custom.inventoryY)
                {
                    for (int k = Custom.spaceX; k <= Custom.spaceX + Custom.inventoryX; k++)
                    {
                        for (int l = 0; l <= Custom.inventorySpaceX; l++)
                        {
                            if (l == 0)
                            {
                                Console.SetCursorPosition(k * (Custom.inventorySpaceX + 1), i * (Custom.inventorySpaceY + 1) + j);
                                Console.Write(Custom.bracketTwo); //INSIDE CONSOLE Y BRACKET
                            }
                            else if (k != Custom.spaceX + Custom.inventoryX)
                            {
                                Console.SetCursorPosition(k * (Custom.inventorySpaceX + 1) + l, i * (Custom.inventorySpaceY + 1) + j);
                                Console.Write(Custom.bracketFour); //INSIDE CONSOLE EMPTY BRACKET
                            }
                        }
                    }
                }
            }
        }
    }

    //--DRAW THE JUICE INSIDE OF THE INVENTORY FRAME--
    static void DrawInsideInventory()
    {
        for (int i = 0; i < Custom.spaceY * 2 + Custom.inventoryY; i++)
        {
            for (int j = 0; j <= Custom.inventorySpaceY; j++)
            {
                if (j != 0)
                {
                    for (int k = 0; k < Custom.spaceX * 2 + Custom.inventoryX; k++)
                    {
                        for (int l = 0; l <= Custom.inventorySpaceX; l++)
                        {
                            if (l != 0)
                            {
                                Console.SetCursorPosition(k * (Custom.inventorySpaceX + 1) + l, i * (Custom.inventorySpaceY + 1) + j);
                                if (Universal.placer[i, k] == true)
                                {
                                    if (SpaceAvailable(k, i)) //IF THE SPACE IS CURRENTLY AVAILABLE, MEANING IT NEITHER HAS AN ITEM OCCUPYING IT NOR IS IT OUTSIDE THE INVENTORY
                                    {
                                        Console.Write(Universal.stack ? Custom.bracketTwelve : Custom.bracketFive); //IF THE SPACE IS BOTH AVAILABLE AND SELECTED IN THE PLACER
                                    }
                                    else
                                    {
                                        Console.Write(Universal.error ? Custom.bracketEight : Universal.stack ? Custom.bracketTwelve : Custom.bracketSeven); //IF THE SPACE IS SELECTED IN THE PLACER BUT NOT AVAILABLE
                                    }
                                }
                                else if (SpaceInsideInv(k, i) && Universal.selector[i - Custom.spaceY, k - Custom.spaceX] == true)
                                {
                                    if (SpaceAvailable(k, i))
                                    {
                                        Console.Write(Custom.bracketNine); //IF THE SPACE IS SELECTED IN THE SELECTOR AND AVAILABLE
                                    }
                                    else
                                    {
                                        Console.Write(Custom.bracketTen); //IF THE SPACE IS SELECTED IN THE SELECTOR BUT NOT AVAILABLE
                                    }
                                }
                                else
                                {
                                    if (SpaceAvailable(k, i))
                                    {
                                        Console.Write(Custom.bracketFour); //IF THE SPACE IS NOT SELECTED BUT AVAILABLE
                                    }
                                    else
                                    {
                                        if (SpaceInsideInv(k, i))
                                        {
                                            Console.BackgroundColor = Universal.Item.itemList[ImprintsItem(Universal.ImprintData.imprintArray[i - Custom.spaceY, k - Custom.spaceX])].invColor;
                                            Console.Write(Universal.Item.itemList[ImprintsItem(Universal.ImprintData.imprintArray[i - Custom.spaceY, k - Custom.spaceX])].invChar); //IF THE SPACE IS NEITHER SELECTED NOR AVAILABLE BUT HAS AN ITEM IN IT
                                            Console.ResetColor();
                                        }
                                        else
                                        {
                                            Console.Write(Custom.bracketFour); //IF THE SPACE IS NEITHER SELECTED NOR AVAILABLE AND HAS NOTHING IN IT
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    //--CLEAR THE TEXT SPACE AT THE BOTTOM OF THE CONSOLE--
    static void ClearTextSpace()
    {
        for (int i = Universal.invConBottom + 1; i < Universal.consoleHeight; i++)
        {
            for (int j = Universal.invConLeft + 1; j < Universal.invConRight; j++)
            {
                Console.SetCursorPosition(j, i);
                Console.Write(' ');
            }
        }
    }
    //--ADD TEXT TO THE TEXT SPACE AT THE BOTTOM OF THE CONSOLE--
    static void AddText(int atP_row, int atP_col, string atP_text)
    {
        Console.SetCursorPosition(Universal.invConLeft + atP_col + 1, Universal.invConBottom + atP_row + 1);
        Console.Write(atP_text);
    }
    //--DISPLAY THE INFORMATION OF EACH ITEM THAT IS SELECTED--
    static void DisplayItemInfo(int diiP_imprint)
    {
        ClearTextSpace();
        int dii_left = Custom.spaceX * (Custom.inventorySpaceX + 1) + 1;
        int dii_right = (Custom.spaceX + Custom.inventoryX) * (Custom.inventorySpaceX + 1) + 1 - 13;
        int dii_top = (Custom.spaceY + Custom.inventoryY) * (Custom.inventorySpaceY + 1) + 1;
        int dii_bottom = (Custom.spaceY * 2 + Custom.inventoryY) * (Custom.inventorySpaceY + 1) + 1;

        AddText(0, 0, Universal.Item.itemList[ImprintsItem(diiP_imprint)].name);

        int dii_x = dii_left;
        int dii_y = dii_top + 3;

        int dii_lineLength = 0;

        string[] dii_words = Universal.Item.itemList[Universal.ImprintData.imprintList[diiP_imprint].itemNumber].desc.Split(' ');

        foreach (string word in dii_words)
        {
            if (dii_lineLength + word.Length > dii_right - dii_left)
            {
                dii_y++;
                dii_x = dii_left;
                dii_lineLength = 0;

                if (dii_y > dii_bottom)
                {
                    return;
                }
            }

            Console.SetCursorPosition(dii_x, dii_y);
            Console.Write(word + " ");
            dii_x += word.Length + 1;
            dii_lineLength += word.Length + 1;
        }

        AddText(0, (Custom.inventoryX) * (Custom.inventorySpaceX + 1) + 1 - 12, Convert.ToString(Universal.ImprintData.imprintList[diiP_imprint].stackAmount));
        AddText(1, (Custom.inventoryX) * (Custom.inventorySpaceX + 1) + 1 - 12, "/" + Universal.Item.itemList[ImprintsItem(diiP_imprint)].maxStack);

        for (int i = 0; i < Custom.spaceY * (Custom.inventorySpaceY + 1) + 1; i++)
        {
            AddText(i, (Custom.inventoryX) * (Custom.inventorySpaceX + 1) + 1 - 13, "|");
        }

        for (int i = 0; i < Custom.inventoryX * (Custom.inventorySpaceX + 1) + 1 - 13; i++)
        {
            AddText(1, i, "_");
        }
    }
    //--CLEAR ALL OF THE TEXT INSIDE OF THE TEXT SPACE--
    static void ClearText(int ctP_startingRow, int ctP_endingRow, int ctP_startingCol, int ctP_endingCol)
    {
        for (int i = Universal.invConBottom + 1 + ctP_startingRow; i <= Universal.invConBottom + ctP_endingRow; i++)
        {
            for (int j = Universal.invConLeft + 1 + ctP_startingCol; j <= Universal.invConLeft + ctP_endingCol; j++)
            {
                Console.SetCursorPosition(j, i);
                Console.Write(' ');
            }
        }
    }

    //--USER INTERACTION MAIN MENU--
    static void UserMain()
    {
        ConsoleKey um_keyInput = ConsoleKey.D0;

        ClearTextSpace();

        AddText(0, 0, "Press A to add a new item");
        AddText(1, 0, "Press E to interact with the inventory");
        AddText(2, 0, "Press G to sort items in a random order");

        while (true)
        {
            um_keyInput = KeyInput();
            switch (um_keyInput)
            {
                case ConsoleKey.E:
                    UserInteract();
                    break;
                case ConsoleKey.A:
                    UserAddItem();
                    break;
                case ConsoleKey.G:
                    AutoSort();
                    break;
            }
        }
    }
    //--ONE OF THE ACTIONS OF THE USER, INTEACTING WITH THE INVENTORY AND VIEWING DESCRIPTIONS--
    static void UserInteract()
    {
        ConsoleKey ui_keyInput = ConsoleKey.D0;

        int ui_currentImprint = 0;

        int ui_cursorXOffset = 0;
        int ui_cursorYOffset = 0;

        DrawInventory();

        while (true)
        {
            for (int i = 0; i < Custom.inventoryY; i++)
            {
                for (int j = 0; j < Custom.inventoryX; j++)
                {
                    Universal.selector[i, j] = false;
                }
            }

            Universal.selector[ui_cursorYOffset, ui_cursorXOffset] = true;

            ui_currentImprint = Universal.ImprintData.imprintArray[ui_cursorYOffset, ui_cursorXOffset];

            DrawInsideInventory();

            if (ui_currentImprint != -1)
            {
                DisplayItemInfo(ui_currentImprint);
            }
            else
            {
                ClearTextSpace();
            }

            ui_keyInput = KeyInput();

            switch (ui_keyInput)
            {
                case ConsoleKey.Enter:
                    if (Universal.ImprintData.imprintArray[ui_cursorYOffset, ui_cursorXOffset] != -1)
                    {
                        Universal.selector[ui_cursorYOffset, ui_cursorXOffset] = false;
                        UserPlaceItem(Universal.ImprintData.imprintArray[ui_cursorYOffset, ui_cursorXOffset], false);
                    }
                    break;
                case ConsoleKey.Escape:
                    Universal.selector[ui_cursorYOffset, ui_cursorXOffset] = false;
                    DrawInsideInventory();
                    UserMain();
                    break;
                case ConsoleKey.UpArrow:
                    if (ui_cursorYOffset > 0)
                    {
                        ui_cursorYOffset--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (ui_cursorYOffset + 1 < Custom.inventoryY)
                    {
                        ui_cursorYOffset++;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (ui_cursorXOffset > 0)
                    {
                        ui_cursorXOffset--;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (ui_cursorXOffset + 1 < Custom.inventoryX)
                    {
                        ui_cursorXOffset++;
                    }
                    break;
            }
        }
    }
    //--ANOTHER ONE OF THE USERS MAIN ACTIONS, PICKING A NEW ITEM TO INITIALIZE INTO THE INVENTORY--
    static void UserAddItem()
    {
        int uad_selectedRow = 0;
        int uad_topOfList = Custom.inventoryY * (Custom.inventorySpaceY + 1) + 2;
        ConsoleKey uad_keyInput = ConsoleKey.D0;

        uad_keyInput = ConsoleKey.D0;

        int uad_imprint;

        ClearTextSpace();

        for (int i = 0; i < Universal.Item.itemList.LongCount(); i++)
        {
            AddText(i, 5, Universal.Item.itemList[i].name);
        }

        while (true)
        {
            ClearText(0, Universal.Item.itemList.Count, 0, 5);
            AddText(uad_selectedRow, 0, "Add->");

            uad_keyInput = KeyInput();

            switch (uad_keyInput)
            {
                case ConsoleKey.Enter:
                    uad_imprint = CreateImprint(uad_selectedRow);
                    UserPlaceItem(uad_imprint, true);
                    break;
                case ConsoleKey.Escape:
                    UserMain();
                    break;
                case ConsoleKey.DownArrow:
                    if (uad_selectedRow < Universal.Item.itemList.LongCount() - 1)
                    {
                        uad_selectedRow++;
                    }
                    else
                    {
                        uad_selectedRow = 0;
                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (uad_selectedRow > 0)
                    {
                        uad_selectedRow--;
                    }
                    else
                    {
                        uad_selectedRow = (int)Universal.Item.itemList.LongCount() - 1;
                    }
                    break;
            }
        }
    }
    //--ANOTHER ONE OF THE USERS MAIN ACTIONS, PLACING AN ITEM--
    static void UserPlaceItem(int upiP_imprint, bool upiP_newItem)
    {
        bool[,] upi_itemSpaces;
        int finderLeft = Custom.inventoryX + 1;
        int finderTop = Custom.inventoryY + 1;
        int finderRight = -1;
        int finderBottom = -1;
        int upi_itemXOffset = Custom.spaceX;
        int upi_itemYOffset = Custom.spaceY;
        int upi_placeStatus;
        ConsoleKey upi_keyInput = ConsoleKey.D0;

        Universal.placing = true;

        if (upiP_newItem)
        {
            upi_itemSpaces = Universal.Item.itemList[ImprintsItem(upiP_imprint)].spaces;
        }
        else
        {
            for (int i = 0; i < Custom.inventoryY; i++)
            {
                for (int j = 0; j < Custom.inventoryX; j++)
                {
                    if (Universal.ImprintData.imprintArray[i, j] == upiP_imprint)
                    {
                        if (i < finderTop)
                        {
                            finderTop = i;
                        }
                        if (j < finderLeft)
                        {
                            finderLeft = j;
                        }
                        if (i > finderBottom)
                        {
                            finderBottom = i;
                        }
                        if (j > finderRight)
                        {
                            finderRight = j;
                        }
                    }
                }
            }
            upi_itemSpaces = new bool[finderBottom - finderTop + 1, finderRight - finderLeft + 1];
            upi_itemXOffset = finderLeft + Custom.spaceX;
            upi_itemYOffset = finderTop + Custom.spaceY;
            for (int i = finderTop; i <= finderBottom; i++)
            {
                for (int j = finderLeft; j <= finderRight; j++)
                {
                    if (upiP_imprint == Universal.ImprintData.imprintArray[i, j])
                    {
                        Universal.ImprintData.imprintArray[i, j] = -1;
                        upi_itemSpaces[i - finderTop, j - finderLeft] = true;
                    }
                }
            }
        }

        DrawInventory();

        while (true)
        {
            for (int i = 0; i < Universal.placer.GetLength(0); i++)
            {
                for (int j = 0; j < Universal.placer.GetLength(1); j++)
                {
                    Universal.placer[i, j] = false;
                }
            }
            for (int i = 0; i < upi_itemSpaces.GetLength(0); i++)
            {
                for (int j = 0; j < upi_itemSpaces.GetLength(1); j++)
                {
                    if (i + upi_itemYOffset < Universal.placer.GetLength(0) && j + upi_itemXOffset < Universal.placer.GetLength(1))
                    {
                        Universal.placer[i + upi_itemYOffset, j + upi_itemXOffset] = upi_itemSpaces[i, j];
                    }
                }
            }

            DrawInsideInventory();

            Universal.error = false;
            Universal.stack = false;

            upi_keyInput = KeyInput();
            switch (upi_keyInput)
            {
                case ConsoleKey.Enter:
                    upi_placeStatus = Place(upiP_imprint);
                    if (upi_placeStatus == 1)
                    {
                        Universal.error = true;
                    }
                    else if (upi_placeStatus == 2)
                    {
                        Universal.stack = true;
                    }
                    else if (upi_placeStatus == 3)
                    {
                        Universal.placing = false;
                        ResetPlacer();
                        DrawInsideInventory();
                        UserMain();
                    }
                    break;
                case ConsoleKey.Escape:
                    Universal.placing = false;
                    ResetPlacer();
                    DrawInsideInventory();
                    UserMain();
                    break;
                case ConsoleKey.R:
                    upi_itemSpaces = Rotated2DBoolArray(upi_itemSpaces);
                    break;
                case ConsoleKey.UpArrow:
                    if (upi_itemYOffset > Custom.spaceY)
                    {
                        upi_itemYOffset--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (upi_itemYOffset + upi_itemSpaces.GetLength(0) < Custom.spaceY + Custom.inventoryY)
                    {
                        upi_itemYOffset++;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (upi_itemXOffset > Custom.spaceX)
                    {
                        upi_itemXOffset--;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (upi_itemXOffset + upi_itemSpaces.GetLength(1) < Custom.spaceX + Custom.inventoryX)
                    {
                        upi_itemXOffset++;
                    }
                    break;
            }
        }
    }

    //--SIMPLY RESET THE PLACER, MAKE A METHOD SINCE IT'S DONE LIKE 4 TIMES--
    static void ResetPlacer()
    {
        for (int i = 0; i < Universal.placerHeight; i++)
        {
            for (int j = 0; j < Universal.placerWidth; j++)
            {
                Universal.placer[i, j] = false;
            }
        }
    }
    
    //--CREATE A NEW ITEM--
    static int CreateImprint(int ciP_itemListPos)
    {
        Universal.ImprintData imprintObj = new Universal.ImprintData(ciP_itemListPos, 1);
        return imprintObj.listPos;
    }

    //--PLACE THE ITEM IN THE INVENTORY OR STACK IT ON TOP OF ANOTHER OF THE SAME TYPE AND RETURN THE STATUS--
    static int Place(int pP_imprint)
    {
        if(CanPlace())
        {
            for (int i = 0; i < Universal.placerHeight; i++)
            {
                for (int j = 0; j < Universal.placerWidth; j++)
                {
                    if (Universal.placer[i, j])
                    {
                        Universal.ImprintData.imprintArray[i - Custom.spaceY, j - Custom.spaceX] = pP_imprint;
                    }
                }
            }
            return 3;
        }
        else
        {
            for (int i = 0; i < Custom.inventoryY; i++)
            {
                for (int j = 0; j < Custom.inventoryX; j++)
                {
                    if (Universal.placer[i + Custom.spaceY, j + Custom.spaceX] && Universal.ImprintData.imprintArray[i, j] != -1)
                    {
                        if (ImprintsItem(Universal.ImprintData.imprintArray[i, j]) == ImprintsItem(pP_imprint))
                        {
                            if (Universal.ImprintData.imprintList[Universal.ImprintData.imprintArray[i, j]].stackAmount != Universal.Item.itemList[ImprintsItem(pP_imprint)].maxStack)
                            {
                                if (Universal.ImprintData.imprintList[Universal.ImprintData.imprintArray[i, j]].stackAmount + Universal.ImprintData.imprintList[pP_imprint].stackAmount <= Universal.Item.itemList[ImprintsItem(Universal.ImprintData.imprintArray[i, j])].maxStack)
                                {
                                    Universal.ImprintData.imprintList[Universal.ImprintData.imprintArray[i, j]].stackAmount += Universal.ImprintData.imprintList[pP_imprint].stackAmount;
                                    return 3;
                                }
                                else if (Universal.ImprintData.imprintList[Universal.ImprintData.imprintArray[i, j]].stackAmount + Universal.ImprintData.imprintList[pP_imprint].stackAmount > Universal.Item.itemList[ImprintsItem(Universal.ImprintData.imprintArray[i, j])].maxStack)
                                {
                                    Universal.ImprintData.imprintList[pP_imprint].stackAmount = Universal.ImprintData.imprintList[Universal.ImprintData.imprintArray[i, j]].stackAmount + Universal.ImprintData.imprintList[pP_imprint].stackAmount - Universal.Item.itemList[ImprintsItem(Universal.ImprintData.imprintArray[i, j])].maxStack;
                                    Universal.ImprintData.imprintList[Universal.ImprintData.imprintArray[i, j]].stackAmount = Universal.Item.itemList[ImprintsItem(Universal.ImprintData.imprintArray[i, j])].maxStack;
                                    return 2;
                                }
                            }
                        }
                    }
                }
            }
            return 1;
        }
    }   
    //--AUTOMATICALLY SORT THE INVENTORY USING RANDOMIZATION--
    static void AutoSort()
    {
        List<int> as_presentImprints;
        HashSet<int> as_presentImprintsSet = new HashSet<int>();
        Random as_rng = new Random();
        int as_rngInt;
        bool as_placed;
        int as_temp;
        bool[,] as_currentSpaces;
        int[,] initialLayout = Universal.ImprintData.imprintArray;

    startSorting:

        for (int i = 0; i < Universal.ImprintData.imprintArray.GetLength(0); i++)
        {
            for (int j = 0; j < Universal.ImprintData.imprintArray.GetLength(1); j++)
            {
                if (initialLayout[i, j] != -1)
                {
                    as_presentImprintsSet.Add(initialLayout[i, j]);
                    Universal.ImprintData.imprintArray[i, j] = -1;
                }
            }
        }

        as_presentImprints = AutoStack(as_presentImprintsSet.ToList());

        for (int i = as_presentImprints.Count - 1; i > 0; i--)
        {
            as_rngInt = as_rng.Next(0, as_presentImprints.Count);
            as_temp = as_presentImprints[i];
            as_presentImprints[i] = as_presentImprints[as_rngInt];
            as_presentImprints[as_rngInt] = as_temp;
        }

        for (int i = 0; i < as_presentImprints.Count; i++)
        {
            as_placed = false;
            as_currentSpaces = Universal.Item.itemList[ImprintsItem(as_presentImprints[i])].spaces;
            for (int j = Custom.spaceY; j < Custom.spaceY + Custom.inventoryY && !as_placed; j++)
            {
                for (int k = Custom.spaceX; k < Custom.spaceX + Custom.inventoryX && !as_placed; k++)
                {
                    for (int l = 0; l < 4 && !as_placed; l++)
                    {
                        as_currentSpaces = Rotated2DBoolArray(as_currentSpaces);
                        ManualPlacer(k, j, as_currentSpaces);
                        if (CanPlace())
                        {
                            ManualImprinter(as_presentImprints[i]);
                            as_placed = true;
                        }
                        if (!as_placed && j == Custom.spaceY + Custom.inventoryY - 1 && k == Custom.spaceX + Custom.inventoryX - 1 && l == 3)
                        {
                            goto startSorting;
                        }
                    }
                }
            }
        }
        DrawInsideInventory();
        UserMain();
    }
    //--STACK ALL OF THE STACKABLE ITEMS DURING THE AUTOMATIC SORTING--
    static List<int> AutoStack(List<int> asP_oldImprints)
    {
        List<int> as_stackableItems = new List<int>();
        List<int> as_currentStackingItems = new List<int>();
        int as_currentTotalStack = 0;

        List<int> as_newImprints = new List<int>();

        for (int i = 0; i < asP_oldImprints.Count; i++)
        {
            if (Universal.Item.itemList[ImprintsItem(asP_oldImprints[i])].maxStack == 1)
            {
                as_newImprints.Add(asP_oldImprints[i]);
            }
            else
            {
                if (!as_stackableItems.Contains(ImprintsItem(asP_oldImprints[i])))
                {
                    as_stackableItems.Add(ImprintsItem(asP_oldImprints[i]));
                }
            }
        }

        for (int i = 0; i < as_stackableItems.Count; i++)
        {
            as_currentStackingItems.Clear();
            as_currentTotalStack = 0;

            for (int j = 0; j < asP_oldImprints.Count; j++)
            {
                if (ImprintsItem(asP_oldImprints[j]) == as_stackableItems[i])
                {
                    as_currentStackingItems.Add(asP_oldImprints[j]);
                    as_currentTotalStack += Universal.ImprintData.imprintList[asP_oldImprints[j]].stackAmount;
                }
            }

            for (int j = 0; j < as_currentStackingItems.Count; j++)
            {
                if (as_currentTotalStack != 0)
                {
                    if (as_currentTotalStack > Universal.Item.itemList[as_stackableItems[i]].maxStack)
                    {
                        as_currentTotalStack -= Universal.Item.itemList[as_stackableItems[i]].maxStack;
                        Universal.ImprintData.imprintList[as_currentStackingItems[j]].stackAmount = Universal.Item.itemList[as_stackableItems[i]].maxStack;
                        as_newImprints.Add(as_currentStackingItems[j]);
                    }
                    else
                    {
                        Universal.ImprintData.imprintList[as_currentStackingItems[j]].stackAmount = as_currentTotalStack;
                        as_currentTotalStack = 0;
                        as_newImprints.Add(as_currentStackingItems[j]);
                    }
                }
            }
        }
        return as_newImprints;
    }
    //--ATTEMPT PLACEMENT DURING THE AUTOMATIC SORTING--
    static void ManualPlacer(int mpP_xOffset, int mpP_yOffset, bool[,] mpP_spaces)
    {
        ResetPlacer();
        for (int i = 0; i < mpP_spaces.GetLength(0); i++)
        {
            for (int j = 0; j < mpP_spaces.GetLength(1); j++)
            {
                if (i + mpP_yOffset < Universal.placer.GetLength(0) && j + mpP_xOffset < Universal.placer.GetLength(1))
                {
                    Universal.placer[i + mpP_yOffset, j + mpP_xOffset] = mpP_spaces[i, j];
                }
            }
        }
    }
    //--IMPRINT ITEMS DURING AUTOMATIC SORTING--
    static void ManualImprinter(int miP_imprint)
    {
        for (int i = 0; i < Universal.placerHeight; i++)
        {
            for (int j = 0; j < Universal.placerWidth; j++)
            {
                if (Universal.placer[i, j])
                {
                    Universal.ImprintData.imprintArray[i - Custom.spaceY, j - Custom.spaceX] = miP_imprint;
                }
            }
        }
        ResetPlacer();
    }

    //--CHECK IF THE ITEM CURRENTLY IN THE PLACER CAN BE PLACED IN THE INVENTORY--
    static bool CanPlace()
    {
        for (int i = 0; i < Universal.placerHeight; i++)
        {
            for (int j = 0; j < Universal.placerWidth; j++)
            {
                if (Universal.placer[i, j])
                {
                    if (!SpaceAvailable(j, i))
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
    //--CHECK IF A SPECIFIC SPACE IS NOT OCCUPIED AND INSIDE THE INVENTORY--
    static bool SpaceAvailable(int saP_x, int saP_y)
    {
        if (SpaceInsideInv(saP_x, saP_y))
        {
            for (int i = 0; i < Custom.inventoryY; i++)
            {
                for (int j = 0; j < Custom.inventoryX; j++)
                {
                    if (Universal.ImprintData.imprintArray[saP_y - Custom.spaceY, saP_x - Custom.spaceX] != -1)
                    {
                        return false;
                    }
                }
            }
        }
        else
        {
            return false;
        }
        return true;
    }
    //--CHECK IF A SPECIFIC SPACE IS INSIDE THE INVENTORY--
    static bool SpaceInsideInv(int siiP_x, int siiP_y)
    {
        if (siiP_y >= Custom.spaceY && siiP_y < Custom.spaceY + Custom.inventoryY && siiP_x >= Custom.spaceX && siiP_x < Custom.spaceX + Custom.inventoryX)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //--RETURN THE CORRESPONDING ITEM NUMBER OF AN IMPRINT (EACH ITEM HAS A UNIQUE ID CORRESPONDING TO ITS DATA THROUGH THE ImprintData CLASS)--
    static int ImprintsItem(int iiP_imprint)
    {
        return Universal.ImprintData.imprintList[iiP_imprint].itemNumber;
    }

    //--ROTATE A 2D BOOL ARRAY 90 DEGREES CLOCKWISE--
    static bool[,] Rotated2DBoolArray(bool[,] r2baP_2DBoolArray)
    {
        bool[,] r2ba_new2DBoolArray = new bool[r2baP_2DBoolArray.GetLength(1), r2baP_2DBoolArray.GetLength(0)];
        for (int i = 0; i < r2ba_new2DBoolArray.GetLength(1); i++)
        {
            for (int j = 0; j < r2ba_new2DBoolArray.GetLength(0); j++)
            {
                r2ba_new2DBoolArray[j, r2ba_new2DBoolArray.GetLength(1) - 1 - i] = r2baP_2DBoolArray[i, j];
            }
        }
        return r2ba_new2DBoolArray;
    }

    //--KEY INPUT FUNCTION--
    static ConsoleKey KeyInput()
    {
        //CLEAR ALL THE KEYS IN THE BUFFER BY READING THEM INDIVIDUALLY, DO NOT USE ReadLine BECAUSE IT PAUSES THE PROGRAM TO WAIT FOR NEW BUFFER INPUT
        while (Console.KeyAvailable)
        {
            Console.ReadKey();
        }
        //PAUSE THE PROGRAM AFTER HAVING CLEARED THE BUFFER TO WAIT FOR NEW BUFFER INPUT BEFORE RETURNING THE FIRST KEY TO ENTER THE BUFFER
        while (!Console.KeyAvailable) { }
        return Console.ReadKey(intercept: true).Key;
    }
}