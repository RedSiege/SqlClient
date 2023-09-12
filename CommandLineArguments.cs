using System;

namespace SqlClient
{
    public class CommandLineArguments
    {
        public static ArgOptions ArgParse(string[] args)
        {
            ArgOptions options = new ArgOptions();
            int lastindex = 0;
            for (int i = 0; i < args.Length; i++)
            {


                if (args[i].Equals("--server"))
                {
                    i++;
                    options.Server = args[i];
                }


                else if (args[i].Equals("--database"))
                {
                    i++;
                    options.DatabaseName = args[i];

                }
                else if (args[i].Equals("--username"))
                {
                    i++;
                    options.UserName = args[i];
                }
                else if (args[i].Equals("--password"))
                {
                    i++;
                    options.Password = args[i];
                }
                else
                {
                    lastindex = i;
                    i = args.Length;
                }
                

            }

            if (lastindex > 0)
            {
                for (int i = lastindex; i <= args.Length - 1; i++)
                {
                    options.Query += " " + args[i];
                }
            }
            

            return options;
        }
    }
}
