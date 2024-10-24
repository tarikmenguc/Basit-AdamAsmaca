using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdamAsmaca
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            KurallarveKarsilama();
            string sehir = getrastgelesehir();  // Rastgele seçilen şehri bir kere alıyoruz
            GorunenSehriGizleme(sehir);  // Şehri gizleyerek gösteriyoruz
            TusBasmaOlayi(sehir);  // Aynı şehri tahmin işlemi için gönderiyoruz

            Console.ReadLine();
        }

        public static void KurallarveKarsilama()
        {
            Console.WriteLine("Adam Asmaca oyununa hoş geldiniz.");
            Console.WriteLine("Başlamadan önce kuralları bilmen gerekiyor.");
            Console.WriteLine("Kurallar:");
            Console.WriteLine("Bir şehir seçilecek ve şehir adı gizli bir şekilde “_” ile gösterilecektir.\r\nOyuncu her turda bir harf tahmininde bulunacak.\r\n3 yanlış tahmin hakkı bulunmaktadır. 3 yanlış tahmin yapan oyuncu elenir.\r\nOyuncu istediği zaman 0 tuşuna basarak tüm kelimeyi tahmin etme moduna geçebilir.\r\nTüm kelimeyi tahmin ederken yalnızca bir deneme hakkı vardır. Yanlış tahmin yaparsa direkt elenir.");
        }

        public static void GorunenSehriGizleme(string donendeger)
        {
            Console.WriteLine(donendeger);  
            for (int i = 0; i < donendeger.Length; i++)
            {
                Console.Write("|_|");
            }
            Console.WriteLine(" ");
            Console.WriteLine($"Kelimemiz {donendeger.Length} harften oluşuyor tahmin için herhangi bir tuşa basınız.");
        }

        public static string getrastgelesehir()
        {
            Sehirler sehirler = new Sehirler();
            Random rnd = new Random();
            int index = rnd.Next(sehirler.TumSehirler.Length);
            return sehirler.TumSehirler[index];  
        }

        public static void TusBasmaOlayi(string donendeger)
        {
            char[] gizlenenKelime = new string('_', donendeger.Length).ToCharArray();  // Şehir adını gizliyoruz, _ ile
            int denemeHakki = 3;  // 3 yanlış tahmin hakkı

            while (denemeHakki > 0 && gizlenenKelime.Contains('_'))  // Hem hak bitmediği sürece hem de kelimenin tamamı çözülmediyse
            {
                Console.WriteLine("\nBir harf tahmin edin:");
                char basilantus = Console.ReadLine()[0];  // Kullanıcının girdiği harfi alıyoruz

                bool harfBulundu = false;  // Tahmin edilen harf bulundu mu kontrol etmek için

                for (int i = 0; i < donendeger.Length; i++)
                {
                    if (char.ToLower(donendeger[i]) == char.ToLower(basilantus))  // Harfi karşılaştır (büyük-küçük harf farkını göz ardı et)
                    {
                        gizlenenKelime[i] = donendeger[i];  // Eğer doğruysa ilgili pozisyonu aç
                        harfBulundu = true;
                    }
                }

                if (harfBulundu)
                {
                    Console.WriteLine("Doğru tahmin! Güncel kelime: " + new string(gizlenenKelime));  // Güncellenen kelimeyi göster
                }
                else
                {
                    denemeHakki--;
                    Console.WriteLine($"Yanlış tahmin! Kalan hakkınız: {denemeHakki}");
                }

                // Eğer kelimenin tamamı tahmin edilmişse oyunu bitir
                if (!gizlenenKelime.Contains('_'))
                {
                    Console.WriteLine("Tebrikler, kelimeyi doğru tahmin ettiniz!");
                    break;
                }
            }

            // Eğer deneme hakkı bittiyse, oyuncu kaybetti
            if (denemeHakki == 0)
            {
                Console.WriteLine($"Maalesef bilemediniz. Doğru kelime: {donendeger}");
            }

            // Konsolun kapanmasını önlemek için
            Console.WriteLine("Oyunu bitirmek için bir tuşa basın...");
            Console.ReadKey();
        }
    }
}
