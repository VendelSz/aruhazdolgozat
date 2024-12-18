﻿namespace aruhazdolgozat
{
	class Program
	{
		static string[] raktarTermekek = { "Tej", "Kenyér", "Tojás", "Víz", "Cukor" };
		static double[] raktarArak = { 150, 200, 300, 100, 250 };
		static int[] raktarKeszletek = { 10, 20, 15, 50, 30 };

		static List<Tuple<string, int>> kosar = new List<Tuple<string, int>>();
		static void Main(string[] args)
		{
			while (true)
			{

				Console.WriteLine("Áruház rendszer");
				Console.WriteLine("Válassz műveletet:");
				Console.WriteLine("1. Raktár termékeinek megtekintése");
				Console.WriteLine("2. Termék hozzáadása a kosárhoz");
				Console.WriteLine("3. Kosár tartalmának megjelenítése");
				Console.WriteLine("4. Termék eltávolítása a kosárból");
				Console.WriteLine("5. Kosár ürítése");
				Console.WriteLine("6. Vásárlás szimuláció");
				Console.WriteLine("7. Legdrágább termék a raktárban");
				Console.WriteLine("8. Legolcsóbb termék a raktárban");
				Console.WriteLine("9. Kosár statisztikája és teljes értéke");
				Console.WriteLine("10. Raktárkészlet ellenőrzése");
				Console.WriteLine("11. Új termék felvétele a raktárba");
				Console.WriteLine("12. Termékek rendezése ár szerint");
				Console.WriteLine("13. Kilépés");
				Console.WriteLine();

				int valasz = int.Parse(Console.ReadLine());

				switch (valasz)
				{
					case 1:
						TermekekListazasa();
						break;
					case 2:
						TermekHozzaadasaKosarhoz();
						break;
					case 3:
						KosarTartalma();
						break;
					case 4:
						TermekEltavolitasaKosarbol();
						break;
					case 5:
						KosarUritese();
						break;
					case 6:
						VasarlasSzimulacio();
						break;
					case 7:
						LegdragabbTermek();
						break;
					case 8:
						LegolcsobbTermek();
						break;
					case 9:
						KosarStatisztika();
						break;
					case 10:
						RaktarKeszletEllenorzes();
						break;
					case 11:
						UjTermek();
						break;
					case 12:
						TermekekRendezese();
						break;
					case 13:
						Console.WriteLine("Kilépés...");
						return;
					default:
						Console.WriteLine("Érvénytelen választás, próbádl újra!");
						break;
				}
			}
		}

		static void TermekekListazasa()
		{
			Console.WriteLine();
			Console.WriteLine("Raktár tartalma:");
			for (int i = 0; i < raktarTermekek.Length; i++)
			{
				Console.WriteLine($"{raktarTermekek[i]} - {raktarArak[i]} Ft - Készlet: {raktarKeszletek[i]} db");
			}
			Console.WriteLine();
		}

		static void TermekHozzaadasaKosarhoz()
		{
			Console.WriteLine();
			Console.WriteLine("Add meg a termék nevét:");
			string termekNev = Console.ReadLine();
			bool termekLetezik = false;
			for (int i = 0; i < raktarTermekek.Length; i++)
			{
				if (raktarTermekek[i].ToLower() == termekNev.ToLower())
				{
					termekLetezik = true;

					Console.WriteLine("Add meg a mennyiséget:");
					int mennyiseg = int.Parse(Console.ReadLine());

					if (raktarKeszletek[i] >= mennyiseg)
					{
						kosar.Add(new Tuple<string, int>(termekNev, mennyiseg));
						raktarKeszletek[i] -= mennyiseg;
						Console.WriteLine("A termék hozzáadva a kosárhoz.");
					}
					else
					{
						Console.WriteLine("Nincs elég készlet.");
					}
					break;
				}
			}

			if (!termekLetezik)
			{
				Console.WriteLine("A termék nem található a raktárban.");
			}
			Console.WriteLine();
		}

		static void KosarTartalma()
		{
			Console.WriteLine();
			Console.WriteLine("Kosár tartalma:");
			foreach (var item in kosar)
			{
				Console.WriteLine($"{item.Item1} - {item.Item2} db");
			}
			Console.WriteLine();
		}

		static void TermekEltavolitasaKosarbol()
		{
			Console.WriteLine();
			Console.WriteLine("Add meg a törölni kívánt termék nevét:");
			string termekNev = Console.ReadLine();
			bool termekTalalva = false;
			for (int i = 0; i < kosar.Count; i++)
			{
				if (kosar[i].Item1.ToLower() == termekNev.ToLower())
				{
					termekTalalva = true;
					Console.WriteLine("Add meg a törölni kívánt mennyiséget:");
					int mennyiseg = int.Parse(Console.ReadLine());

					if (kosar[i].Item2 >= mennyiseg)
					{
						kosar[i] = new Tuple<string, int>(kosar[i].Item1, kosar[i].Item2 - mennyiseg);
						for (int j = 0; j < raktarTermekek.Length; j++)
						{
							if (raktarTermekek[j].ToLower() == termekNev.ToLower())
							{
								raktarKeszletek[j] += mennyiseg;
								break;
							}
						}
						Console.WriteLine("A termék eltávolítva a kosárból.");
					}
					else
					{
						Console.WriteLine("Nincs elég a termékből a kosárban.");
					}
					break;
				}
			}
			if (!termekTalalva)
			{
				Console.WriteLine("A termék nincs a kosárban.");
			}
			Console.WriteLine();
		}

		static void KosarUritese()
		{
			Console.WriteLine();
			kosar.Clear();
			Console.WriteLine("A kosár kiürítve.");
			Console.WriteLine();
		}

		static void VasarlasSzimulacio()
		{
			Console.WriteLine();
			double osszeg = 0;
			Console.WriteLine("Vásárlás részletei:");
			foreach (var item in kosar)
			{
				for (int i = 0; i < raktarTermekek.Length; i++)
				{
					if (raktarTermekek[i].ToLower() == item.Item1.ToLower())
					{
						double termekAr = raktarArak[i];
						double termekOsszeg = item.Item2 * termekAr;
						Console.WriteLine($"{item.Item1} - {item.Item2} db - {termekAr} Ft/db - Összesen: {termekOsszeg} Ft");
						osszeg += termekOsszeg;
						break;
					}
				}
			}
			Console.WriteLine($"Végösszeg: {osszeg} Ft");
			KosarUritese();
			Console.WriteLine();
		}

		static void LegdragabbTermek()
		{
			Console.WriteLine();
			double legdragabbAr = 0;
			string legdragabbTermek = "";
			for (int i = 0; i < raktarArak.Length; i++)
			{
				if (raktarArak[i] > legdragabbAr)
				{
					legdragabbAr = raktarArak[i];
					legdragabbTermek = raktarTermekek[i];
				}
			}
			Console.WriteLine($"A legdrágább termék a raktárban: {legdragabbTermek} - {legdragabbAr} Ft");
			Console.WriteLine();
		}

		static void LegolcsobbTermek()
		{
			Console.WriteLine();
			double legolcsobbAr = double.MaxValue;
			string legolcsobbTermek = "";
			for (int i = 0; i < raktarArak.Length; i++)
			{
				if (raktarArak[i] < legolcsobbAr)
				{
					legolcsobbAr = raktarArak[i];
					legolcsobbTermek = raktarTermekek[i];
				}
			}
			Console.WriteLine($"A legolcsóbb termék a raktárban: {legolcsobbTermek} - {legolcsobbAr} Ft");
			Console.WriteLine();
		}

		static void KosarStatisztika()
		{
			Console.WriteLine();
			int osszesTermek = 0;
			foreach (var item in kosar)
			{
				osszesTermek += item.Item2;
			}
			int kulonbozoTermekekSzama = kosar.Count;
			double osszErtek = 0;
			foreach (var item in kosar)
			{
				for (int i = 0; i < raktarTermekek.Length; i++)
				{
					if (raktarTermekek[i].ToLower() == item.Item1.ToLower())
					{
						double termekAr = raktarArak[i];
						osszErtek += item.Item2 * termekAr;
						break;
					}
				}
			}
			Console.WriteLine($"A kosárban összesen: {osszesTermek} db termék található.");
			Console.WriteLine($"Különböző termékek száma: {kulonbozoTermekekSzama} db.");
			Console.WriteLine($"A kosár összértéke: {osszErtek} Ft");
			Console.WriteLine();
		}

		static void RaktarKeszletEllenorzes()
		{
			Console.WriteLine();
			Console.WriteLine("Alacsony készletű termékek (5 db alatt):");
			for (int i = 0; i < raktarKeszletek.Length; i++)
			{
				if (raktarKeszletek[i] > 0 && raktarKeszletek[i] < 5)
				{
			            Console.WriteLine($"{raktarTermekek[i]} - Készlet: {raktarKeszletek[i]} db");
			        }
			}
			Console.WriteLine();
		}

		static void UjTermek()
		{
		Console.WriteLine();
		Console.WriteLine("Add meg az új termék nevét:");
		string ujTermekNev = Console.ReadLine();
		
		Console.WriteLine("Add meg az új termék árát:");
		double ujTermekAr = double.Parse(Console.ReadLine());
		
		Console.WriteLine("Add meg az új termék készletét:");
		int ujTermekKeszlet = int.Parse(Console.ReadLine());
		
		Array.Resize(ref raktarTermekek, raktarTermekek.Length + 1);
		Array.Resize(ref raktarArak, raktarArak.Length + 1);
		Array.Resize(ref raktarKeszletek, raktarKeszletek.Length + 1);
		
		raktarTermekek[^1] = ujTermekNev;
		raktarArak[^1] = ujTermekAr;
		raktarKeszletek[^1] = ujTermekKeszlet;
		
		Console.WriteLine("Az új termék hozzáadva a raktárhoz.");
		Console.WriteLine();
		}

		static void TermekekRendezese()
		{
		Console.WriteLine();
		Console.WriteLine("A termékek ár szerint növekvő sorrendben rendezve:");
		
		for (int i = 0; i < raktarArak.Length - 1; i++)
		{
			for (int j = i + 1; j < raktarArak.Length; j++)
			{
			        if (raktarArak[i] > raktarArak[j])
			        {
				        double tempAr = raktarArak[i];
				        raktarArak[i] = raktarArak[j];
				        raktarArak[j] = tempAr;
				
				        string tempTermek = raktarTermekek[i];
				        raktarTermekek[i] = raktarTermekek[j];
				        raktarTermekek[j] = tempTermek;
				
				        int tempKeszlet = raktarKeszletek[i];
				        raktarKeszletek[i] = raktarKeszletek[j];
				        raktarKeszletek[j] = tempKeszlet;
			        }
			}
		}
		
		TermekekListazasa();
		Console.WriteLine();
		}
	}
}



