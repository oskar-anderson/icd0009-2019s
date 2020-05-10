using System;
using System.Collections.Generic;

namespace Domain.Base.App.EF.Helpers
{
    public class Pictures
    {
        readonly List<string> pizzas = new List<string>()    // steal!, steal!!, STEAL!!! https://www.crust.com.au
        {
            "https://d2mekbzx20fc11.cloudfront.net/uploads/web.Vietnamese-Chilli-Chicken_600x600.png",
            "https://d2mekbzx20fc11.cloudfront.net/uploads/web.Pesto-Chicken-Club_2018_600x600.png",
            "https://d2mekbzx20fc11.cloudfront.net/uploads/web.Peri-Peri_600x600_NEW.png",
            "https://d2mekbzx20fc11.cloudfront.net/uploads/web.BBQ_600x600.png",
            "https://d2mekbzx20fc11.cloudfront.net/uploads/Chorizo_600x600.png",
            "https://d2mekbzx20fc11.cloudfront.net/uploads/Quattro_600x600.png",
            "https://d2mekbzx20fc11.cloudfront.net/uploads/web.Mediterranean-Lamb_2018_600x600.png",
            "https://d2mekbzx20fc11.cloudfront.net/uploads/web.Mexican_600x600_NEW.png",
            "https://d2mekbzx20fc11.cloudfront.net/uploads/web.Supreme_600x600_NEW.png",
            "https://d2mekbzx20fc11.cloudfront.net/uploads/web.Meat-Deluxe_600x600.png",
            "https://d2mekbzx20fc11.cloudfront.net/uploads/web.Pepperoni_600x600.png",
            "https://d2mekbzx20fc11.cloudfront.net/uploads/Garlic_Confit_Prawn_600x600.png",
            "https://d2mekbzx20fc11.cloudfront.net/uploads/web.Szechuan-Chilli-Prawn_600x600.png",
            "https://d2mekbzx20fc11.cloudfront.net/uploads/PaneerMarsala_600x600.png",
            "https://d2mekbzx20fc11.cloudfront.net/uploads/web.Vegetarian-Supreme_600x600.png",
            "https://d2mekbzx20fc11.cloudfront.net/uploads/web.c1889-Margherita_600x600.png",
            "https://d2mekbzx20fc11.cloudfront.net/uploads/VeganPeriPeri_600x600.png",
            "https://d2mekbzx20fc11.cloudfront.net/uploads/web.Smokey-BBQ-Pulled-Jackfruit_600x600.png",
            "https://d2mekbzx20fc11.cloudfront.net/uploads/web.Margherita_600x600.png",
            "https://d2mekbzx20fc11.cloudfront.net/uploads/web.Capricciosa_600x600.png",
            "https://d2mekbzx20fc11.cloudfront.net/uploads/web.Hawaiian_600x600.png",
            
        };

        public string GetRandomPizza()
        {
            var random = new Random();
            return pizzas[random.Next(pizzas.Count)];
        }
    }
}