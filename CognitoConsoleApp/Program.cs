using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime;
using Newtonsoft.Json;
using System;
using System.IO;

namespace CognitoConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter cognito App Id:");
            string cognitoAppId = Console.ReadLine();
            Console.WriteLine("App ID is " + cognitoAppId + ".");
            Console.Write("Enter Cognito User Pool Id:");
            string userPoolId = Console.ReadLine();
            Console.WriteLine("Cognito User Pool Id is " + userPoolId + ".");
            Console.Write("Username:");
            string username = Console.ReadLine();
            Console.WriteLine("Username is " + username + ".");
            Console.Write("password:");
            string pw = Console.ReadLine();
            Console.WriteLine("pw is " + pw + ".");


            var cognitoClient = new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials(), RegionEndpoint.USWest2);
            var cognitoUserPool = new CognitoUserPool(userPoolId, cognitoAppId, cognitoClient);
            var currentUser = new CognitoUser(username, cognitoAppId, cognitoUserPool, cognitoClient);
            var response = currentUser.StartWithSrpAuthAsync(new InitiateSrpAuthRequest()
            {
                Password = pw
            }).Result;

            string js = JsonConvert.SerializeObject(response);
            Console.WriteLine(js);
            File.WriteAllText("out.txt", js);
            Console.ReadLine();
        }
    }
}
