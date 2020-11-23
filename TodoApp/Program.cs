using System;

namespace TodoApp
{
	class Program
	{

		static void DisplayMenu()
		{
			Console.WriteLine("Command Line Todo Application");
			Console.WriteLine("=============================\n");
			Console.WriteLine("Command line arguments:");
			Console.WriteLine("\t-l\tLists all the tasks");
			Console.WriteLine("\t-a\tAdds a new task");
			Console.WriteLine("\t-r\tRemoves a task");
			Console.WriteLine("\t-c\tCompletes a task");

		}

		static void Main(string[] args)
		{
			if (args.Length <= 1)
			{
				DisplayMenu();
			}
			else
			{
				switch (args[1])
				{
					case "-l":
						ListTasks();
						break;
					case "-a":
						if (args.Length > 2)
						{
							AddTask(args[2]);
						}
						else
						{
							Console.WriteLine("Unable to add: no task provided");
						}
						break;
					case "-r":
						if (args.Length > 2)
						{
							bool success = int.TryParse(args[2], out int index);
							if (success)
							{
								RemoveTask(index - 1);
							}
							else
							{
								Console.WriteLine("Unable to remove: index is not a number");
							}
						}
						else
						{
							Console.WriteLine("Unable to remove: no index provided");
						}
						
						break;
					case "-c":
						if (args.Length > 2)
						{
							bool success = int.TryParse(args[2], out int index);
							if (success)
							{
								CheckTask(index - 1);
							}
							else
							{
								Console.WriteLine("Unable to check: index is not a number");
							}
						}
						else
						{
							Console.WriteLine("Unable to check: no index provided");
						}

						break;
					default:
						Console.WriteLine("Unsupported argument");
						DisplayMenu();
						break;
				}
			}

		static void ListTasks()
		{
			string[] lines = System.IO.File.ReadAllLines(@"todos.txt");
			if (lines.Length == 0)
			{
				Console.WriteLine("No todos for today!");
			}
			for (int i = 0; i < lines.Length; i++)
			{
				// Use a tab to indent each line of the file.
				Console.WriteLine($"{i+1} - {lines[i]}");
			}
		}
	}

		private static void CheckTask(int index)
		{
			string[] lines = System.IO.File.ReadAllLines(@"todos.txt");
			if (index >= lines.Length)
			{
				Console.WriteLine("Unable to check: index is out of bound");
				return;
			}
			using System.IO.StreamWriter file =
			new System.IO.StreamWriter(@"todos.txt");
			for (int i = 0; i < lines.Length; i++)
			{
				string line = lines[i];
				if (i == index)
				{
					line = line.Replace("[ ]", "[x]");
				}
				file.WriteLine(line);
			}
		}

		private static void RemoveTask(int index)
		{
			string[] lines = System.IO.File.ReadAllLines(@"todos.txt");
			if (index >= lines.Length)
			{
				Console.WriteLine("Unable to remove: index is out of bound");
				return;
			}
			using System.IO.StreamWriter file =
			new System.IO.StreamWriter(@"todos.txt");
			for (int i = 0; i < lines.Length; i++)
			{
				if (i != index)
				{
					file.WriteLine(lines[i]);
				}
			}
		}

		private static void AddTask(string task)
		{
			using System.IO.StreamWriter file = new System.IO.StreamWriter(@"todos.txt", true);
			file.WriteLine($"[ ] {task}");
		}
	}
}
