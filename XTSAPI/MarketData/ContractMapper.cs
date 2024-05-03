using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DateTimeConverter = CsvHelper.TypeConversion.DateTimeConverter;

namespace XTSAPI.MarketData
{
    public sealed class ContractMapper : ClassMap<ContractInfo>
    {
        public ContractMapper() {
            Map(m => m.ExchangeSegment).Index(0).TypeConverter<ExchangeSegmentConverter>();//a
            Map(m => m.ExchangeInstrumentID).Index(1);//b
            Map(m => m.InstrumentType).Index(2).TypeConverter<IntConverter>();//c
            Map(m => m.Name).Index(3)/*.TypeConverter<RemoveSpaces>()*/;//d
            Map(m => m.Description).Index(4);//e
            Map(m => m.Series).Index(5);//f
            Map(m => m.NameWithSeries).Index(6);//g
            Map(m => m.InstrumentID).Index(7).TypeConverter<LongConverter>();//h
            Map(m => m.PriceBand.High).Index(8).TypeConverter<doublePriceConverter>();//i
            Map(m => m.PriceBand.Low).Index(9).TypeConverter<doublePriceConverter>();//j
            Map(m => m.FreezeQty).Index(10).TypeConverter<doublePriceConverter>();//k
            Map(m => m.TickSize).Index(11).TypeConverter<doublePriceConverter>();//l
            Map(m => m.LotSize).Index(12).TypeConverter<IntConverter>();//m
            Map(m => m.MultiPlier).Index(13).TypeConverter<doublePriceConverter>();//n
            Map(m => m.UnderlyingInstrumentId).Index(14).TypeConverter<LongConverter>();//o
            Map(m => m.UnderlyingIndexName).Index(15).TypeConverter<NameConverter>();//p
            Map(m => m.ContractExpiration).Index(16).TypeConverter<DateTimeConverter>();//q
            Map(m => m.StrikePrice).Index(17).TypeConverter<doublePriceConverter>();//r
            Map(m => m.OptionType).Index(18).TypeConverter<IntConverter>();//s
           
            //Map(m => m.ExchangeName).Index(20).TypeConverter<NameConverter>();
        }
    }




    public class ExchangeSegmentConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return -1;
            }
            switch (text)
            {
                case "NSECM":
                    return ExchangeSegment.NSECM;
                    
                case "NSEFO":
                    return ExchangeSegment.NSEFO;
                    
                case "NSECD":
                    return ExchangeSegment.NSECD;
                    
                case "NSECO":
                    return ExchangeSegment.NSECO;
                    
                case "SLBM":
                    return ExchangeSegment.SLBM;
                    
                case "BSECM":
                    return ExchangeSegment.BSECM;
                    
                case "BSEFO":
                    return ExchangeSegment.BSEFO;
                    
                case "BSECD":
                    return ExchangeSegment.BSECD;
                    
                case "BSECO":
                    return ExchangeSegment.BSECO;
                    
                case "NCDEX":
                    return ExchangeSegment.NCDEX;
                    
                case "MSEICM":
                    return ExchangeSegment.MSEICM;
                    
                case "MSEIFO":
                    return ExchangeSegment.MSEIFO;
                    
                case "MSEICD":
                    return ExchangeSegment.MSEICD;
                    
                case "MCXFO":
                    return ExchangeSegment.MCXFO;
                    
                default:
                    throw new ArgumentException($"Invalid Exchange Type{text}");

            }
        }
    }




    public class LongConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return -1L;
            }
            double value;
            if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
            {
                return (long)value;
            }
            return null;
        }
    }

    public class DateTimeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return DateTime.MinValue;
            }
            DateTime result;
            if(DateTime.TryParse(text,out result))
            {
                return result;
            }
            return null;
        }
    }

    public class doublePriceConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return 0.00;
            }
            double value;
            if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
            {
                return value;
            }
            return null;
        }
    }

    public class IntConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return -1;
            }
            int result;
            if(int.TryParse(text, out result))
            {
                return result;
            }
            return null;
        }
    }

    public class NameConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "NONE";
            }
            else {
                return text;
            }
        }
    }

    public class RemoveSpaces : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "NONE";
            }
            else
            {
                return text.Replace(" ","");
            }
        }
    }
}
