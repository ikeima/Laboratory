using iTextSharp.text;
using iTextSharp.text.pdf;
using Laboratory.DbModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace Laboratory.Classes
{
    static class AccountantOperation
    {
        // Метод для генерирования PDF-отчёта
        public static void GeneratePDF(Dictionary<int, decimal> patientsServicesSum, Insurance_companies company, DateTime beginDate, DateTime endDate, Accounts account)
        {
            
            using (FolderBrowserDialog folder = new FolderBrowserDialog())
            {
                System.Windows.MessageBox.Show("Выберите место для сохранения отчёта!");
                string pathToSave;
                if (folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    pathToSave = folder.SelectedPath;

                    string ttfNormal = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF"); // Установка шрифта

                    var baseFont = BaseFont.CreateFont(ttfNormal, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                    var font = new Font(baseFont, Font.DEFAULTSIZE, Font.NORMAL); // Настройка размера и стиля шрифта. Здесь - нормальный
                    var fontBold = new Font(baseFont, Font.DEFAULTSIZE, Font.BOLD); // Здесь - полужирный

                    var document = new Document(PageSize.A4, 20, 20, 30, 20);

                    using (var writer = PdfWriter.GetInstance(document, new FileStream(pathToSave + @"\Отчёт для счёта № " + account.Number + ".pdf", FileMode.Create)))
                    {
                        document.Open();
                        document.NewPage();

                        Paragraph p = new Paragraph("Счёт № " + account.Number + " от " + DateTime.Today.ToShortDateString() + "\n", font);
                        var c = new Chunk("Поставщик: ООО \"" + company.Title + "\", ИНН: " + company.TIN + ", адрес: " + company.Adress + "\n", font);
                        var c1 = new Chunk("Клиент: ООО “Лаборатория №17”, ИНН: 12121212, адрес: 119019, город Москва, улица Новый Арбат, дом 99, тел.: +7999212121\n");
                        var c2 = new Chunk("Основание: Федеральный закон №1499-1 от 28 июня 1991 г. \"О медицинском страховании граждан в Российской Федерации\"\n\n");
                        p.Add(c);
                        p.Add(c1);
                        p.Add(c2);
                        document.Add(p);

                        PdfPTable table = new PdfPTable(3); // Создание таблицы с треся колонками

                        PdfPCell cell = new PdfPCell(new Phrase("№", font));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase("ФИО пациентов", font));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Сумма за оказанные пациенту услуги", font));
                        table.AddCell(cell);

                        decimal servicesSum = 0;
                        int i = 1;
                        foreach (var item in patientsServicesSum)
                        {
                            var patient = ApiOperation.GetPatient(item.Key.ToString());

                            cell = new PdfPCell(new Phrase(i.ToString(), font));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase(patient.Last_name + " " + patient.First_name + " " + patient.Patronymic, font));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase(item.Value.ToString(), font));
                            table.AddCell(cell);
                            servicesSum += item.Value;
                            i++;

                        }
                        document.Add(table);
                        Paragraph p2 = new Paragraph("Период оплаты: " + beginDate.ToShortDateString() + "-" + endDate.ToShortDateString(), font);
                        Paragraph p3 = new Paragraph("Общая стоимость услуг: " + servicesSum.ToString("C", CultureInfo.CreateSpecificCulture("ru")), fontBold);
                        p3.Alignment = Element.ALIGN_RIGHT;

                        document.Add(p2);
                        document.Add(p3);

                        document.Close();
                        writer.Close();

                        System.Windows.MessageBox.Show("Ваш отчёт сохранен по пути: " + pathToSave);
                    }
                }
            }
            


        }
    }
}
