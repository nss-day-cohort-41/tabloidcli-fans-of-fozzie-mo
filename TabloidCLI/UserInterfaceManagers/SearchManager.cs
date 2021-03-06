﻿using System;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class SearchManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private TagRepository _tagRepository;

        public SearchManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _tagRepository = new TagRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Search Menu");
            Console.WriteLine(" 1) Search Blogs");
            Console.WriteLine(" 2) Search Authors");
            Console.WriteLine(" 3) Search Posts");
            Console.WriteLine(" 4) Search All");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    SearchBlogs();
                    return this;
                case "2":
                    Console.Clear();
                    SearchAuthors();
                    return this;
                case "3":
                    Console.Clear();
                    SearchPosts();
                    return this;
                case "4":
                    Console.Clear();
                    SearchAll();
                    return this;
                case "0":
                    Console.Clear();
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void SearchAuthors()
        {
            Console.Write("Enter tag name> ");
            string tagName = Console.ReadLine();

            SearchResults<Author> results = _tagRepository.SearchAuthors(tagName);

            if (results.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                results.Display();
            }
        }

        private void SearchBlogs()
        {
            Console.Write("Enter tag name> ");
            string tagName = Console.ReadLine();

            SearchResults<Blog> results = _tagRepository.SearchBlogs(tagName);

            if (results.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                results.Display();
            }
        }

        private void SearchPosts()
        {
            Console.Write("Enter tag name> ");
            string tagName = Console.ReadLine();

            SearchResults<Post> results = _tagRepository.SearchPosts(tagName);

            if (results.NoResultsFound)
            {
                Console.WriteLine($" No results for {tagName}");
            }
            else
            {
                results.Display();
            }
        }

        private void SearchAll()
        {
            Console.Write("Enter tag name> ");
            string tagName = Console.ReadLine();

            SearchResults<Author> resultsAuthor = _tagRepository.SearchAuthors(tagName);
            SearchResults<Blog> resultsBlog = _tagRepository.SearchBlogs(tagName);
            SearchResults<Post> resultsPost = _tagRepository.SearchPosts(tagName);

            if (resultsBlog.NoResultsFound && resultsAuthor.NoResultsFound && resultsPost.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                resultsBlog.Display();
                resultsAuthor.Display();
                resultsPost.Display();
            }
        }
    }
}