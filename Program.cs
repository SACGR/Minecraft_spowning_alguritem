// See https://aka.ms/new-console-template for more information




using System.Data;
using System.Runtime.CompilerServices;
using System.Linq;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;
//variable
//kordionaterna som ett fiende spownas vid 
int xVerde;
int yVerde;
int zVerde;
//hur många försök pär chunk
const int försök = 20;
//hur stor yta försöker den spowna på 
const int maxDim = 16;
//Vad tror du de är 
int mobCap = 70;
//de högsta blocket
int[,] heaitManp = new int[maxDim, maxDim];
//vär är block plaserade, 0 är betydera att den inte fins ett black, 1 är ett helt block
int[,,] blockPlatcering = new int[maxDim, 384, maxDim];
List<int> levandeFiender = new();
int hurLängeSakerLevenr = 2;
int packSise = 4;
Console.WriteLine("den Startar");

//dettta settar hella hitmapen till att barra vara 1
for (int i = 0; i < heaitManp.Length; i++)
{
    heaitManp[i / maxDim, i % maxDim] = 1;
}
Console.WriteLine("Hitmap är genererad");
for (int xi = 0; xi < maxDim; xi++)
{
    for (int zi = 0; zi < maxDim; zi++)
    {
        blockPlatcering[xi, 0, zi] = 1;

    }
}
Console.WriteLine("Kartan är nu genereread");




for (int Tik = 0; Tik < 100; Tik++)
{
    //prrintear ut vilket tick och mabcap
    Console.WriteLine("Tik = {2} cap {0}/{1}",levandeFiender.Count, mobCap,Tik);
    //tarbort alla fiender som inte borde leva 
    levandeFiender = levandeFiender.Where(x => x > Tik).ToList();



    //prövar att spowna fiender ett par gånger 
    for (int i = 0; i < försök; i++)
    {

        FörsökSpowna(out xVerde, out yVerde, out zVerde, heaitManp, maxDim);
        //försöker plasera en fiende på de slumpade kordinaten
        if (blockPlatcering[xVerde, yVerde, zVerde] == 0 && blockPlatcering[xVerde, yVerde - 1, zVerde] == 1 && levandeFiender.Count <= mobCap)
        {
            //deta är om du bara vill spowna en fiende
            //levandeFiender.Add(Tik + hurLängeSakerLevenr);

            //printar att en fiende har spownat och var
            //Console.WriteLine("{5}-(J) {0} {1} {2} cap {3}/{4}", xVerde, yVerde, zVerde,levandeFiender.Count, mobCap,Tik);


            //försöker göra pack spownign
            PackSpowning(levandeFiender, out levandeFiender, xVerde, yVerde, zVerde,packSise,blockPlatcering,Tik,hurLängeSakerLevenr,mobCap);


        }

        else
        {
            //printear att en finede försökte spowna 
            Console.WriteLine("{5}-(N) {0} {1} {2} cap {3}/{4}", xVerde, yVerde, zVerde,levandeFiender.Count, mobCap,Tik);
        }
    }
}
Console.WriteLine("klar");

//den här funktionen väljer ett x, y och z värde 
static void FörsökSpowna(out int x, out int y, out int z, int[,] heaitManp, int maxDim)
{
    Random rnd = new Random();
    x = rnd.Next(0, maxDim);
    z = rnd.Next(0, maxDim);
    y = rnd.Next(0, (heaitManp[x, z] + 1));
}
static void PackSpowning(List<int> levandeFiender,out List<int> levandeFienderUt,int oX,int oY,int oZ,int packSise, int[,,] blockPlatcering,int Tik,int hurLängeSakerLevenr, int mobCap)
{
    levandeFienderUt = levandeFiender;

    for(int i = 0; i < packSise; i++){
    oX = linjerFördelnig(oX);
    oZ = linjerFördelnig(oZ);

    if (Enumerable.Range(0,blockPlatcering.GetLength(0)).Contains(oX) && Enumerable.Range(0,blockPlatcering.GetLength(2)).Contains(oZ) && blockPlatcering[oX, oY, oZ] == 0 && blockPlatcering[oX, oY - 1, oZ] == 1)
        {
            levandeFienderUt.Add(Tik + hurLängeSakerLevenr);
            //printar att en fiende har spownat och var
            Console.WriteLine("{5}-(J) {0} {1} {2} pSF-{6}/{7} cap {3}/{4}", oX, oY, oZ,levandeFiender.Count, mobCap,Tik,i,packSise);
        }
    else
        Console.WriteLine("{5}-(N) {0} {1} {2} pSF-{6}/{7} cap {3}/{4}", oX, oY, oZ,levandeFiender.Count, mobCap,Tik,i,packSise);
    }

    


}
static int linjerFördelnig(int startVärde){
    Random rnd = new Random();
    int cordinaten;
    if(rnd.Next(0,1)==1){
        cordinaten = (int)(Gamma.Sample(1,5)*-1 +startVärde);
    }
    else
        cordinaten = (int)(Gamma.Sample(1,5)+startVärde);
    return(cordinaten);

}
    



