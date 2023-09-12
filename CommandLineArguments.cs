using System;

namespace SqlClient
{
    public class CommandLineArguments
    {
        public static ArgOptions ArgParse(string[] args)
        {
            ArgOptions options = new ArgOptions();
            for (int i = 0; i < args.Length; i++)
            {

                switch (args[i])
                {
                    case "--server":
                        i++;
                        options.Server = args[i];
                        break;
                    case "--database":
                        i++;
                        options.DatabaseName = args[i];
                        break;
                    case "--username":
                        i++;
                        options.UserName = args[i];
                        break;
                    case "--password":
                        i++;
                        options.Password = args[i];
                        break;
                    case "--query":
                        i++;
                        options.Query = args[i];
                        break;
                    default:
                        throw new Exception($"Option {args[i]} it not recognized");

                }
            }

            return options;
        }
    }
}
