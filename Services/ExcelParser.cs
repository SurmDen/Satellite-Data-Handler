using OfficeOpenXml;
using SQLite_Parser.Models;
using System.Diagnostics.Metrics;
using SQLite_Parser.Interfaces;
using System.Drawing;

namespace SQLite_Parser.Services
{
    public class ExcelParser : IExcelParser
    {

        public void UpdateExcelDocument(string path, Sattellite updatedSattelite)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            FileInfo fileInfo = new FileInfo(path);

            if (fileInfo.Exists && fileInfo.FullName.Contains(".xlsx"))
            {
                using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                {
                    ExcelWorkbook workbook = excelPackage.Workbook;

                    ExcelWorksheet worksheet = workbook.Worksheets.FirstOrDefault();

                    int placeToSetNewSignals = worksheet.Dimension.Rows + 1;

                    if (updatedSattelite != null)
                    {
                        foreach (ExcelSignalModel satSignal in updatedSattelite.Signals)
                        {
                            int neededRow = satSignal.RowNumber;

                            if (neededRow == 0)
                            {
                                neededRow = placeToSetNewSignals;
 
                                worksheet.Cells[neededRow, 1].Value = updatedSattelite.Way;

                                worksheet.Cells[neededRow, 2].Value = updatedSattelite.UnderlyingPoint;

                                worksheet.Cells[neededRow, 3].Value = updatedSattelite.SatteliteName;

                                worksheet.Cells[neededRow, 6].Value = satSignal.FrequencyMHz;

                                worksheet.Cells[neededRow, 9].Value = satSignal.SpeedKHz;

                                worksheet.Cells[neededRow, 10].Value = satSignal.SNR;

                                worksheet.Cells[neededRow, 11].Value = satSignal.Modulation;

                                worksheet.Cells[neededRow, 29].Value = satSignal.Acception;

                                worksheet.Cells[neededRow, 30].Value = satSignal.OperativeMean;

                                worksheet.Cells[neededRow, 31].Value = satSignal.AcceptionTime;

                                placeToSetNewSignals++;
                            }

                            worksheet.Cells[neededRow, 29].Value = satSignal.Acception;

                            worksheet.Cells[neededRow, 31].Value = satSignal.AcceptionTime;
                        }
                    }

                    excelPackage.Save();
                }
            }
        }

        public List<Sattellite> GetSattelitesWithParamsFromExcel(string path)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            FileInfo fileInfo = new FileInfo(path);

            FileInfo info = new FileInfo(path);

            List<Sattellite> sattellites = new List<Sattellite>();

            if (info.Exists && info.FullName.Contains(".xlsx"))
            {
                using (ExcelPackage package = new ExcelPackage(info))
                {
                    ExcelWorkbook workbook = package.Workbook;

                    ExcelWorksheet worksheet = workbook.Worksheets.FirstOrDefault();

                    int rowsCount = worksheet.Dimension.Rows;

                    for (int i = 2; i <= rowsCount; i++)
                    {

                        //sattellite parameters

                        string way = worksheet.Cells[$"A{i}"].Text;

                        string satName = worksheet.Cells[$"C{i}"].Text;

                        if (string.IsNullOrEmpty(way) || string.IsNullOrEmpty(satName))
                        {
                            Console.WriteLine($"empty data: cell[A/C, {i}]");

                            continue;
                        }

                        string point_str = worksheet.Cells[$"B{i}"].Text;

                        double point = 0;

                        try
                        {
                            point = double.Parse(point_str);
                        }
                        catch (Exception)
                        {
                            string new_point_str = point_str.Replace('.', ',');

                            if (!double.TryParse(new_point_str, out point))
                            {
                                continue;
                            }
                        }

                        //signal parameters

                        string range = worksheet.Cells[$"D{i}"].Text;

                        string polaris = worksheet.Cells[$"E{i}"].Text;

                        string freq_str = worksheet.Cells[$"F{i}"].Text;

                        double freq = 0;

                        try
                        {
                            freq = double.Parse(freq_str);
                        }
                        catch (Exception)
                        {
                            string upgraded_freq_str = freq_str.Replace('.', ',');

                            if (!double.TryParse(upgraded_freq_str, out freq))
                            {
                                freq = 0;
                            }
                        }

                        string freq_l = worksheet.Cells[$"G{i}"].Text;

                        string snos = worksheet.Cells[$"H{i}"].Text;

                        string speedString =  worksheet.Cells[$"I{i}"].Text;

                        double speed = 0;

                        if (!string.IsNullOrEmpty(speedString))
                        {
                            try
                            {
                                speed = double.Parse(speedString);
                            }
                            catch (Exception)
                            {
                                string newSpeedString = speedString.Replace('.', ',');

                                if (double.TryParse(newSpeedString, out speed))
                                {
                                    speed = 0;
                                }
                            }
                        }
                        else
                        {
                            speed = 0;
                        }

                        double snr = worksheet.Cells[$"J{i}"].Value == null ? 0 : (double)worksheet.Cells[i, 10].Value;

                        string mod = worksheet.Cells[$"K{i}"].Value == null ? "" : (string)worksheet.Cells[i, 11].Value;

                        string dop_mod = worksheet.Cells[$"L{i}"].Text;

                        string type_puk = worksheet.Cells[$"M{i}"].Text;

                        string pu_dek = worksheet.Cells[$"N{i}"].Text;

                        string cc = worksheet.Cells[$"O{i}"].Text;

                        string p_c = worksheet.Cells[$"P{i}"].Text;

                        string ac = worksheet.Cells[$"Q{i}"].Text;

                        string ibs = worksheet.Cells[$"R{i}"].Text;

                        string idr = worksheet.Cells[$"S{i}"].Text;

                        string syncrop = worksheet.Cells[$"T{i}"].Text;

                        string dcme = worksheet.Cells[$"U{i}"].Text;

                        string oks = worksheet.Cells[$"V{i}"].Text;

                        string from = worksheet.Cells[$"W{i}"].Text;

                        string to = worksheet.Cells[$"X{i}"].Text;

                        string protocols = worksheet.Cells[$"Y{i}"].Text;

                        string ipsec = worksheet.Cells[$"Z{i}"].Text;

                        string inform = worksheet.Cells[$"AA{i}"].Text;

                        string comment = worksheet.Cells[$"AB{i}"].Text;

                        string acception = worksheet.Cells[$"AC{i}"].Text;

                        string operMean = worksheet.Cells[$"AD{i}"].Text;

                        string acceptionTime = worksheet.Cells[$"AE{i}"].Text;

                        string operatop = worksheet.Cells[$"AF{i}"].Text;

                        if (freq != 0 && speed != 0)
                        {
                            ExcelSignalModel signalModel = new ExcelSignalModel()
                            {
                                Range = range,
                                Polarisation = polaris,
                                FrequencyMHz = freq,
                                BH_L = freq_l,
                                Snos = snos,
                                SpeedKHz = speed,
                                SNR = snr,
                                Modulation = mod,
                                Dop_Mod = dop_mod,
                                Type_PUK = type_puk,
                                PU_Dek = pu_dek,
                                CC = cc,
                                P_C = p_c,
                                AC = ac,
                                IBS = ibs,
                                IDR = idr,
                                Syncrop = syncrop,
                                DCME = dcme,
                                OKS = oks,
                                From = from,
                                To = to,
                                Protocols = protocols,
                                IPSec = ipsec,
                                Information = inform,
                                Comment = comment,
                                Acception = acception,
                                OperativeMean = operMean,
                                AcceptionTime = acceptionTime,
                                Operator = operatop,
                                RowNumber = i
                            };

                            //parsing

                            Sattellite? sat = new Sattellite();

                            if (sattellites.Count > 0)
                            {
                                try
                                {
                                    sat = sattellites.
                                    First(s => s.SatteliteName == satName && s.UnderlyingPoint == point && s.Way == way);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }

                            if (!string.IsNullOrEmpty(sat.SatteliteName))
                            {
                                sat.Signals.Add(signalModel);
                            }
                            else
                            {
                                sat = new Sattellite()
                                {
                                    SatteliteName = satName,
                                    UnderlyingPoint = point,
                                    Way = way,
                                    Signals = new List<ExcelSignalModel>() { signalModel }
                                };

                                sattellites.Add(sat);

                            }
                        }
                    }
                }
            }

            sattellites.Sort((x, y) =>
            {
                if (x.Way.ToLower() == "w" && y.Way.ToLower() == "w")
                {
                    if (x.UnderlyingPoint > y.UnderlyingPoint)
                    {
                        return -1;
                    }
                    else if (x.UnderlyingPoint < y.UnderlyingPoint)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if (x.Way.ToLower() == "e" && y.Way.ToLower() == "e")
                {
                    if (x.UnderlyingPoint > y.UnderlyingPoint)
                    {
                        return 1;
                    }
                    else if (x.UnderlyingPoint < y.UnderlyingPoint)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if (x.Way.ToLower() == "e" && y.Way.ToLower() == "w")
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            });

            return sattellites;
        }


        public void UpdateDataToSattelite(Sattellite sattellite, List<SignalModel> models, string path)
        {
            FileInfo info = new FileInfo(path);

            List<ExcelSignalModel> handleredSattelliteSignalModels = new List<ExcelSignalModel>();

            List<SignalModel> handleredDbSignalModels = new List<SignalModel>();

            if (info.Exists && info.FullName.Contains(".xlsx"))
            {
                if (sattellite != null && sattellite.Signals.Count() > 0 && models != null && models.Count() > 0)
                {
                    foreach (ExcelSignalModel model in sattellite.Signals)
                    {
                        double? freq = model.FrequencyMHz;

                        double? speed = model.SpeedKHz;

                        foreach (SignalModel signal in models)
                        {
                            if (signal.FrequencyMHz >= (freq - freq * 0.01) && 
                                signal.FrequencyMHz <= (freq + freq * 0.01) &&
                                signal.SpeedMBod >= (speed - speed * 0.01) &&
                                signal.SpeedMBod <= (speed + speed * 0.01))
                            {
                                handleredSattelliteSignalModels.Add(model);

                                handleredDbSignalModels.Add(signal);

                                model.Acception = signal.TimeLast.ToShortDateString();
                            }
                        }
                    }

                    foreach (ExcelSignalModel exModel in sattellite.Signals.Except(handleredSattelliteSignalModels))
                    {
                        exModel.Acception = "ВЫКЛ";
                    }

                    foreach (SignalModel sigModel in models.Except(handleredDbSignalModels))
                    {
                        sattellite.Signals.Add(new ExcelSignalModel()
                        {
                            FrequencyMHz = sigModel.FrequencyMHz,
                            SpeedKHz = sigModel.SpeedMBod,
                            Modulation = sigModel.ModeType,
                            Acception = sigModel.TimeLast.ToShortDateString(),
                            AcceptionTime = sigModel.TimeLast.ToShortDateString(),
                            OperativeMean = "НЕИЗВ",
                            SNR = sigModel.SNR,
                            RowNumber = 0
                        });
                    }
                }
            }
        }
    }
}
