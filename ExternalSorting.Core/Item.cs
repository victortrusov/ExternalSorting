namespace ExternalSorting.Core
{
    public record Item
    {
        public int Number { get; init; }
        public string Text { get; init; } = "";


        public override string ToString() => $"{Number}. {Text}\n";

        public static explicit operator string(Item item) => item.ToString();
    }
}
