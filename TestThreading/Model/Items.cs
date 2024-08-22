namespace TestThreading.Model;

public partial class Items
{
    public string V1 { get; set; }
    public int V2 { get; set; }
    public string V3 { get; set; }
    public bool V4 { get; set; }
    public int ID { get; set; }

    public string V5 { get; set; }

    public DateTime V6 { get; set; }
    public DateTime? V7 { get; set; }
    public string V8
    {
        get
        {
            return V7?.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }

    public string EntryDate
    {
        get
        {
            return V6.ToString("dd/MM/yyyy");
        }
    }
    public byte V9 { get; set; }
    public string V10
    {
        get
        {
            string _status = string.Empty;
            switch (V9)
            {
                case 0: _status = "U"; break;
                case 1: _status = "V"; break;
                case 2: _status = "B"; break;
                case 3: _status = "O"; break;
                default: break;
            }
            return _status;
        }
    }
    public string V11
    {
        get
        {
            string _status = string.Empty;
            switch (V9)
            {
                case 0: _status = "Unverified "; break;
                case 1: _status = "Verified "; break;
                case 2: _status = "Billed "; break;
                case 3: _status = "Old Transfer "; break;
                default: break;
            }
            return _status;
        }
    }
    public byte? Ret { get; set; }
    public string V12
    {
        get
        {
            return Ret == 1 ? "R" : "C";
        }
    }

    public string V13 { get; set; }
    public short? V14 { get; set; }

    public int V15 { get; set; }
    public string V16
    {
        get
        {
            return V58 + "/" + V15;
        }
    }

    public int V17 { get; set; }
    public double V18 { get; set; }
    public string V19 { get; set; }
    public string V20
    {
        get
        {
            return $"{V21} {V22}";
        }
    }
    public string V21 { get; set; }
    public string V22 { get; set; }
    public string V23 { get; set; }
    public string V24 { get; set; }
    public string V25 { get; set; }

    public string V26 { get; set; }
    public string V27 { get; set; }
    public string V28 { get; set; }
    public string V29 { get; set; }
    public string V30 { get; set; }
    public string V31 { get; set; }

    public string V32 { get; set; }
    public string V33 { get; set; }
    public string V34 { get; set; }
    public string V35 { get; set; }
    public string V36 { get; set; }
    public int V37 { get; set; }
    public string V38 { get; set; }
    public string V39 { get; set; }
    public bool V40 => V38 == "P";

    public string V42 { get { return V41.ToString(); } }
    public int V43 { get; set; }

    public string V44 => $"{V13} (ResponsibleState - {V39})";

    public byte? V45 { get; set; }
    public string V46
    {
        get
        {
            return V45 == 1 ? "3P" : "";
        }
    }
    public int V47 { get; set; }
    public string V48
    {
        get
        {
            return V47 > 0 ? "Y" : "N";
        }
    }
    public DateTime V49 { get; set; }

    public string V50 { get; set; }
    public string V51
    {
        get
        {
            return V49 != DateTime.MinValue ? V49.ToString("dd/MM/yyyy hh:mm tt") + " ReceivedBy: " + V50
                      + Environment.NewLine + "Name: " + V1 : "";
        }
    }
    public string V52 { get; set; }
    public byte V41 { get; set; }
    public string V53
    {
        get
        {
            return "Item printed: " + V41 + Environment.NewLine + "Invoice printed: " + V43 ?? "";
        }
    }
    //public SolidColorBrush MissingItemsColor => Items > MissItems ? new SolidColorBrush(Colors.Salmon) : new SolidColorBrush(Colors.Transparent);

    //public SolidColorBrush RouteCodeColor => RouteCode == "999" ? new SolidColorBrush(Colors.Salmon) : new SolidColorBrush(Colors.Transparent);
    public byte? V54 { get; set; }
    public byte? V55 { get; set; }
    public byte? V56 { get; set; }
    public string V57 { get; set; }
    public int V58 { get; set; }
    public DateTime V59 { get; set; }
    public string V60 { get; set; }

    public override string ToString()
    {
        return V5;
    }

    public bool V61 { get; set; }
    public string V62
    {
        get
        {
            if (V61)
            {
                return "***" + V5;
            }
            return V5;
        }
    }
}
public class Summary
{
    public IEnumerable<Items> Items { get; set; }
    public Total Total { get; set; }
}
public class Total
{
    public int V1 { get; set; }
    public int V2 { get; set; }
    public int V3 { get; set; }
    public int V4 { get; set; }
    public double V5 { get; set; }
}

