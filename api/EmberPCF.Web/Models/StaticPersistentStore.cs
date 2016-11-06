using System;
using System.Collections.Generic;
using System.Linq;

namespace EmberPCF.Web.Models
{
    /// <summary>
    /// Primitive backing store for persistence.
    /// </summary>
    public static class StaticPersistentStore
    {
        private static int currentId { get; set; }

        public static List<Article> Articles { get; set; }

        public static List<Person> People { get; set; }

        public static List<Comment> Comments { get; set; }

        static StaticPersistentStore()
        {
            currentId = 1;

            Articles = new List<Article>();
            People = new List<Person>();
            Comments = new List<Comment>();

            var article1 = new Article("JSON API paints my bikeshed!");
            var article2 = new Article("JSON API makes the tea!");

            var person1 = new Person("Dan", "Gebhardt", "dgeb");
            var person2 = new Person("Rob", "Lang", "brainwipe");

            var comment1 = new Comment("First!");
            var comment2 = new Comment("I like XML Better");

            article1.Author = person1;
            article1.Comments.Add(comment1);
            article1.Body = @"The 12-ounce brown bottle drops a dark brew into the glass, with a vibrant brown hue and a tight and slightly off-white head that settles to a creamy ringed lace. Nose is a bit biscuity, with hints of bark, chicory, toasty bread crusts, nuts, subtle dark chocolate and coffee. Creamy on the palate, with an underlying tingle of carbonation highlighted by an aggressive citric-rind hoppiness and an acrid bite. Malts are toasty, roasty and moderately sweet, offering nutty flavors of biscuit, earth and roots, a bitey coffee backbone, chicory, powdery chocolate and brown bread. The finish is dry and powdery, with lingering hints of biscuit and bark.";
            article1.Comments.Add(comment2);
            Articles.Add(article1);

            article2.Author = person2;
            article2.Body = @"The 11.2-ounce stubby bottle pours a muddy copper with a creamy and frothy beige head and sticky retention. Decanted carefully to keep the dregs (which are plentiful) from pouring out of the bottle. Yeasty, lightly vegetal, metallic, with a subtle spiciness and sweet alcohol in the nose. Light on the palate, with a prickly carbonation and creaminess in its wake. Delicious dark fruitiness of cherries, raisins and ripe plums. Very malty, with undertones of sweet chocolate, mild coffee and caramelized sugars. Crisp, dry edge. Spicy alcohol, a splash of coriander, and a slight tartness that helps tease and tame the sweetness. Soft, drying, powdery feel in the finish. Incredibly more-ish, as in, why, yes we'd love to have another. And with a very deceptive 11 percent alcohol by volume, we will.

Very smooth on the palate with a creamy carbonation buildup, seltzery feel and tonic-water bite. This is followed by a quick and sharp tannic tartness that oddly lingers in the background and into the finish. Layered on top of this is something quite sweet, but not cloying, with a rosewater-like base, notes of strawberries, faint berry-like sourness, white grapes, orange pith, floral honey, over-ripe peach meat, pear juice, hints of mint, and a cutting peppery edge that segues into a somewhat-spicy and increasingly warming booziness, thanks to its 8 percent alcohol by volume. Pleasingly herbal, akin to sweetened cold Chinese tea. Light tannins linger in the bone-dry, powdery finish.

The beer pours a hazy amber with red and brown hues, and yields large carbonation bubbles and a thick, tight, foamy head with some stick and retention. Citric aroma, grassy, with hints of lime and a soft, pale malt beneath. Fairly smooth and creamy on the palate, but a bit lackluster with a light carbonation scrub and watery yet even consistency. The hops come in first: a thin, rind-like citric flavor with a faintly perceived saltiness, light herbal notes and a bitter, grassy bite. Malt character and sweetness is pretty bland, with a weak toasty note as the highlight. Finish is drying, with a lingering hop character and sourness that just doesn't seem right.

A blonde ale fermented with a special strain of yeast, then aged in French oak chardonnay barrels. Flavors of wine and oak absorb into the brew throughout twelve months of aging. During this aging process, a secondary fermentation occurs using a yeast strain disliked by most brewers and winemakers called Brettanomyces.

Served in a 16-ounce pint glass. Cloudy and dirty-peach-colored, topped with a thick, frothy, creamy foam head with great retention and stick, thanks to the hops. Spicy, floral hop aroma with sweetish pale malts beneath. Near full-bodied, incredibly smooth on the palate, creamy and silky. Sharpish citric snap up front, with notes of grapefruit, lime and lemon; deep floral notes as well. A tad resinous, a little piney, especially in the hop finish, which is puckering and drying. Sweet, bready malts with some fruitiness and a doughy yeast toward the end. Lingering hop spiciness and yeast in the finish. Very moreish and bone dry when the last sip fades away.
";
            Articles.Add(article2);


            People.Add(person1);
            People.Add(person2);

            comment1.Author = person1;
            comment2.Author = person2;

            Comments.Add(comment1);
            Comments.Add(comment2);
        }

        public static int GetNextId()
        {
            return currentId++;
        }
    }
}