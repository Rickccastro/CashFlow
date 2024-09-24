using MigraDoc.DocumentObjectModel;
using PdfSharp.Fonts;
using System.Reflection;

namespace CashFlow.Application.UseCase.Expenses.Reports.Pdf.Fonts;

public class ExpensesReportFontResolver : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName);    

        if (stream is null)
        {
            stream = ReadFontFile(FontHelper.DEFAULT_FONT);
        }

        var length = (int)stream!.Length;
        var data = new byte[length];

        stream.Read(buffer:data , offset:0, count: length);

        return data;      
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {     
        return new FontResolverInfo(familyName);
    }


    private Stream? ReadFontFile(string faceName)
    {   
        /*quando o compilador criar o arquivo de dll de baixo nível em assembly 
        pegamos o arquivo de referencia da dll 
         */
        var assembly = Assembly.GetExecutingAssembly();

        /*lendo os bytes do arquivo pelo caminho*/
        return assembly.GetManifestResourceStream($"CashFlow.Application.UseCase.Expenses.Reports.Pdf.Fonts.{faceName}.ttf");
    }
}
