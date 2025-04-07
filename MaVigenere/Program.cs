// See https://aka.ms/new-console-template for more information

using MaVigenere;

Console.WriteLine("Hello, World! C'est Ma Vigenère!");

while (true)
{
    Console.WriteLine("""
                      
                      Select the Operation:
                      1. Encrypt
                      2. Decrypt
                      0. Exit
                      """);
    var operation = Console.ReadLine();
    if(operation is null or "") continue;
    var token = "";
    switch (operation)
    {
        case "1":
            Console.WriteLine("Encryption. \nGive me the raw content:");
            var raw = Console.ReadLine();
            Console.WriteLine("Encryption. \nGive me the token:");
            token = Console.ReadLine();

            var (result, v) = Vigenere.EncryptGiven(raw!, token!);
            Console.WriteLine($"""
                               ===
                               Token: {v.Token}
                               Raw: {v.Raw}
                               Result(Cypher): {result}
                               """);
            break;
        case "2":
            Console.WriteLine("Decryption. \nGive me the cypher:");
            var cyp = Console.ReadLine();
            Console.WriteLine("Decryption. \nGive me the token:");
            token = Console.ReadLine();

            var (res, vig) = Vigenere.DecryptGiven(cyp!, token!);
            Console.WriteLine($"""
                               ===
                               Token: {vig.Token}
                               Cypher: {vig.Cypher}
                               Result(Original Raw): {res}
                               """);
            break;
        case "0":
            Console.WriteLine("bye!");
            return;
        default:
            Console.WriteLine("No such option.");
            break;
    }
}
