using SenderIntegration;

bool running = true;
Sender sender = new Sender();

sender.OpenConnection();
sender.OpenQueue("RandomQueue");


	
	//try
	//{
		while (running)
		{
			string? key = Console.ReadKey().ToString();
			if (key == "ESC")
			{
				running = false;
				sender.DisposeQueue("RandomQueue");
				sender.DisposeChannel();
				sender.DisposeConnection();
			}
			else
				sender.SendMessage("Hello TEST TEST WORLD", "UR.Robot.Status", "URStatus");
		}
	//}
	//finally
	//{
	//	sender.DisposeQueue("RandomQueue");
	//	sender.DisposeChannel();
	//	sender.DisposeConnection();
	//}




