using System.Diagnostics.CodeAnalysis;

namespace MaVigenere;

public class Vigenere(string token)
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public static char GetCharByNum(int of)
    { return (char)of; }

    public static int GetCharNum(char c)
    { return (int)c; }

    private const int Max = 65536;
    public string? Raw { get; set; }
    public string? Cypher { get; set; }

    public string[]? CypherUnicode {
        get
        {
            if (this.Cypher == null) return null;
            List<string> unicodes = new();
            foreach (var single in this.Cypher!)
            {
                unicodes.Add($" u{(int)single} ");
            }

            return unicodes.ToArray();
        }
    }
    public string Token { get; set; } = token!;

    public static (string, Vigenere) EncryptGiven(string rawContent, string token)
    {
        Vigenere v = new(token);
        v.Raw = rawContent;
        v.Encrypt();
        return (v.Cypher ?? "ERR", v);
    }

    public static (string, Vigenere) DecryptGiven(string cypher, string token)
    {
        Vigenere v = new(token);
        v.Cypher = cypher;
        v.Decrypt();
        return (v.Raw ?? "ERR", v);
    }

    public Vigenere Encrypt(string raw)
    {
        this.Raw = raw;
        return Encrypt();
    }

    public Vigenere Decrypt(string cypher)
    {
        this.Cypher = cypher;
        return Decrypt();
    }
    public Vigenere Encrypt()
    {
        if (this.Raw == null) throw new Exception("No Raw Info Set");
        var res = "";
        var index = 0;
        foreach(var x in this.Raw)
        {
            var thisOne = GetCharByNum(x + (this.Token.ToCharArray()[index % this.Token.Length]) % Max);
            res += thisOne;
            index++;
        }

        this.Cypher = res;
        return this;
    }

    public Vigenere Decrypt()
    {
        if (this.Cypher == null) throw new Exception("Nothing to be Decrypted because nothing provided in Cypher");

        var res = "";
        var index = 0;
        foreach (var thisWas in this.Cypher.Select(x => GetCharByNum((x - (this.Token.ToCharArray()[index % this.Token.Length]) + Max) % Max)))
        {
            res += thisWas;
            index++;
        }

        if (this.Raw is not null) Console.WriteLine($"the processed result is: {res} meanwhile the SET result is {this.Raw}\nThese are {((this.Raw == res) ? "" : "NOT ")}same");
        else this.Raw = res;
        return this;
    }

    public static string Decrypt(Vigenere restore)
    { return restore.Decrypt().Raw!; }

    public static string Encrypt(Vigenere secret)
    { return secret.Encrypt().Cypher!; }
}