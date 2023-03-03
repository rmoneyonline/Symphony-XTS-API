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
            Map(m => m.ExchangeSegment).Index(0).TypeConverter<ExchangeSegmentConverter>();
            Map(m => m.ExchangeInstrumentID).Index(1);
            Map(m => m.InstrumentType).Index(2).TypeConverter<IntConverter>();
            Map(m => m.Name).Index(3).TypeConverter<RemoveSpaces>();
            Map(m => m.Description).Index(4);
            Map(m => m.Series).Index(5);
            Map(m => m.NameWithSeries).Index(6);
            Map(m => m.InstrumentID).Index(7).TypeConverter<LongConverter>();
            Map(m => m.PriceBand.High).Index(8).TypeConverter<doublePriceConverter>();
            Map(m => m.PriceBand.Low).Index(9).TypeConverter<doublePriceConverter>();
            Map(m => m.FreezeQty).Index(10).TypeConverter<doublePriceConverter>();
            Map(m => m.TickSize).Index(11).TypeConverter<doublePriceConverter>();
            Map(m => m.LotSize).Index(12).TypeConverter<IntConverter>();
            Map(m => m.MultiPlier).Index(13).TypeConverter<doublePriceConverter>();
            Map(m => m.UnderlyingInstrumentId).Index(14).TypeConverter<LongConverter>();
            Map(m => m.UnderlyingIndexName).Index(15).TypeConverter<NameConverter>();
            Map(m => m.ContractExpiration).Index(16).TypeConverter<DateTimeConverter>();
            Map(m => m.StrikePrice).Index(17).TypeConverter<doublePriceConverter>();
            Map(m => m.OptionType).Index(18).TypeConverter<IntConverter>();
            //Map(m => m.DisplayName).Index(19).TypeConverter<NameConverter>();
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
                    break;
                case "NSEFO":
                    return ExchangeSegment.NSEFO;
                    break;
                case "NSECD":
                    return ExchangeSegment.NSECD;
                    break;
                case "NSECO":
                    return ExchangeSegment.NSECO;
                    break;
                case "SLBM":
                    return ExchangeSegment.SLBM;
                    break;
                case "BSECM":
                    return ExchangeSegment.BSECM;
                    break;
                case "BSEFO":
                    return ExchangeSegment.BSEFO;
                    break;
                case "BSECD":
                    return ExchangeSegment.BSECD;
                    break;
                case "BSECO":
                    return ExchangeSegment.BSECO;
                    break;
                case "NCDEX":
                    return ExchangeSegment.NCDEX;
                    break;
                case "MSEICM":
                    return ExchangeSegment.MSEICM;
                    break;
                case "MSEIFO":
                    return ExchangeSegment.MSEIFO;
                    break;
                case "MSEICD":
                    return ExchangeSegment.MSEICD;
                    break;
                case "MCXFO":
                    return ExchangeSegment.MCXFO;
                    break;
                default:
                    throw new ArgumentException($"Invalid Exchange Type{text}");

            }
            return null;
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
