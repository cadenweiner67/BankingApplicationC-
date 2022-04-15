// <copyright file="Login.cs" company="Caden Weiner">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LoginEngine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;

    /// <summary>
    /// Login Class used for the temp library.
    /// </summary>
    public class Login
    {
        /// <summary>
        /// The dictionary of users and passwords.
        /// </summary>
        private Dictionary<string, (string, string)> users; // in the form username, (password, usertype)

        /// <summary>
        /// The File Used for logins.
        /// </summary>
        private string fileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="Login"/> class.
        /// Login constructor.
        /// </summary>
        /// <param name="fileName">passwordFile.</param>
        public Login(string fileName)
        {
            this.fileName = fileName;
            this.users = new Dictionary<string, (string, string)>();
            Debug.WriteLine("Login Constructor");
        }

        /// <summary>
        /// Verifies that a user can log in.
        /// </summary>
        /// <param name="username">user name.</param>
        /// <param name="password">password.</param>
        /// <returns>whether or not user can be validated and if it can the type of user.</returns>
        public string VerifyLogin(string username, string password)
        {
            if (this.users.ContainsKey(username) && this.users[username].Item1 == password)
            {
                return this.users[username].Item2;
            }
            else
            {
                return "Not Validated";
            }
        }

        /// <summary>
        /// Adds the credentials of a new user.
        /// </summary>
        /// <param name="username">user name to add. must be unique.</param>
        /// <param name="password">password to associate with user.</param>
        /// <param name="userType">User types.</param>
        public void AddUserCredentials(string username, string password, string userType)
        {
            foreach (var k in this.users.Keys)
            {
                Debug.WriteLine(k);
            }

            Debug.WriteLine("Username " + username);
            if (this.users != null && this.users.ContainsKey(username))
            {
                Debug.WriteLine("This username is already registered");
            }
            else if (password == string.Empty)
            {
                Debug.WriteLine("This is not a valid password");
            }
            else
            {
                Debug.WriteLine("Registering new user");
                this.users.Add(username, (password, userType)); // add the user credential
            }
        }

        /// <summary>
        /// This returns the current dictionary.
        /// </summary>
        /// <returns>Dictionary.</returns>
        public Dictionary<string, (string, string)> GetUsers()
        {
            return this.users;
        }

        /// <summary>
        /// Loads all of the users from the file (Used for validation).
        /// </summary>
        public void LoadAllUsers()
        {
            Debug.WriteLine("Fstream opened");
            using (Stream fs = File.Open(this.fileName, FileMode.Open))
            {
                using (XmlReader reader = XmlReader.Create(fs))
                {
                    // we are missing the first cell.
                    string username = string.Empty;
                    string password = string.Empty;
                    string uType = string.Empty;

                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            // Debug.WriteLine("Name of the Element is : " + reader.Name.ToString());
                            string name = reader.Name.ToString();
                            Debug.WriteLine("Name is : " + name.ToString());
                            switch (name)
                            {
                                case "user":
                                    Debug.WriteLine("user");
                                    username = reader.GetAttribute("username").ToString();
                                    password = reader.GetAttribute("password").ToString();
                                    Debug.WriteLine(username + password);
                                    uType = string.Empty;
                                    break;
                                case "type":
                                    uType = reader.ReadString();
                                    Debug.WriteLine("type:" + uType);
                                    this.AddUserCredentials(username, password, uType);
                                    Debug.WriteLine("Credential Added");
                                    break;
                            }
                        }

                        Debug.WriteLine("Read One");
                    }

                    Debug.WriteLine("Done Reading");
                }
            }
        }

        /// <summary>
        /// Store all of the users, including the newly added ones in the file.
        /// </summary>
        public void StoreAllUsers()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineOnAttributes = true;

            // create writer and begin spreadsheet
            File.Delete(this.fileName); // delete the file before creating it. Makes sure it can test creating each time.
            Debug.WriteLine("Deleting " + this.fileName + " if it already exists.");
            FileStream fs;
            Debug.WriteLine("Deleting");
            using (fs = new FileStream(this.fileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite))
            {
                XmlWriter writer = XmlWriter.Create(fs, settings);
                writer.WriteStartDocument();
                writer.WriteStartElement("Users");

                foreach (var k in this.GetUsers().Keys)
                {
                    writer.WriteStartElement("user");
                    writer.WriteAttributeString("username", k.ToString()); // write username
                    writer.WriteAttributeString("password", this.GetUsers()[k].Item1.ToString()); // write password
                    writer.WriteStartElement("type");
                    writer.WriteString(this.GetUsers()[k].Item2.ToString()); // write type of user
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }
        }
    }
}
