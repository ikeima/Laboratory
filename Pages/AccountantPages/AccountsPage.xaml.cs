﻿using Laboratory.Classes;
using Laboratory.DbModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Forms;
using System.IO;

namespace Laboratory.Pages.AccountantPages
{
    /// <summary>
    /// Логика взаимодействия для AccountsPage.xaml
    /// </summary>
    public partial class AccountsPage : Page
    {
        public AccountsPage()
        {
            InitializeComponent();

            accountsList.ItemsSource = ApiOperation.GetAccounts();            
        }
        private void OpenAddAccountWindow(object sender, RoutedEventArgs e)
        {
            AddAccountWindow window = new AddAccountWindow();
            window.ShowDialog();
            accountsList.ItemsSource = ApiOperation.GetAccounts();
        }

        private void DeleteAccount(object sender, RoutedEventArgs e)
        {
            Accounts account = accountsList.SelectedItem as Accounts;

            if (account != null)
            {
                if (System.Windows.MessageBox.Show("Удалить счёт?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    ApiOperation.Delete("accounts", account.Account_id.ToString());
                    accountsList.ItemsSource = ApiOperation.GetAccounts();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Выберите счёт");
            }
        }

        private bool direction = false;
        private void SlideMenu(object sender, RoutedEventArgs e)
        {
            if (!direction)
            {
                ThicknessAnimation anim = new ThicknessAnimation();
                anim.From = new Thickness(0, 0, -170, 0);
                anim.To = new Thickness(0, 0, 0, 0);
                anim.Duration = TimeSpan.FromSeconds(0.5);
                panel.BeginAnimation(MarginProperty, anim);
                menuButton.Content = "→";

                direction = true;
            }
            else
            {
                ThicknessAnimation anim = new ThicknessAnimation();
                anim.From = new Thickness(0, 0, 0, 0);
                anim.To = new Thickness(0, 0, -170, 0);
                anim.Duration = TimeSpan.FromSeconds(0.5);
                panel.BeginAnimation(MarginProperty, anim);
                menuButton.Content = "←";

                direction = false;
            }
        }
        private void Search(object sender, TextChangedEventArgs e)
        {
            var search = from a in ApiOperation.GetAccounts()
                         where (a.Number.Contains(searchBox.Text))
                         select a;

            if (search.Count() == 0)
            {
                System.Windows.MessageBox.Show("Результатов не найдено");
                accountsList.ItemsSource = ApiOperation.GetAccounts();
                searchBox.Text = "";
            }
            else
            {
                accountsList.ItemsSource = search.ToList();
            }
        }

        private void AddReport(object sender, RoutedEventArgs e)
        {
            
        }

        private void PersonalDataProccesingWindowOpen(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog folder = new FolderBrowserDialog())
            {
                System.Windows.MessageBox.Show("Выберите место для сохранения!");
                string pathToSave;
                if (folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    pathToSave = folder.SelectedPath;

                    string ttfNormal = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF"); // Установка шрифта

                    var baseFont = BaseFont.CreateFont(ttfNormal, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                    var font = new Font(baseFont, 10, Font.NORMAL); // Настройка размера и стиля шрифта. Здесь - нормальный
                    var underlineFont = new Font(baseFont, 6, Font.NORMAL); // Здесь - с подчерком снизу
                    var fontBold = new Font(baseFont, Font.DEFAULTSIZE, Font.BOLD); // Здесь - полужирный

                    var document = new Document(PageSize.A4, 20, 20, 30, 20);

                    using (var writer = PdfWriter.GetInstance(document, new FileStream(pathToSave + @"\Согласие на обработку данных" + ".pdf", FileMode.Create)))
                    {
                        document.Open();
                        document.NewPage();

                        Paragraph p = new Paragraph("СОГЛАСИЕ НА ОБРАБОТУ ПЕРСОНАЛЬНЫХ ДАННЫХ", font);
                        p.Alignment = Element.ALIGN_CENTER;

                        Paragraph p1 = new Paragraph("Я, _________________________________________________ , паспорт ____________________ выдан " +
                            "_________________________________________, адрес регистрации:_______________________________________________________, даю свое согласие на обработку в ООО \"Лаборатория №17\"" +
                            "(далее - Оператор) моих персональных данных, относящихся исключительно к перечисленным ниже категориям персональных данных: фамилия, имя, отчество; дата рождения; тип документа, удостоверяющего личность, адрес электронной почты" +
                            "контактный телефон, реквизиты полиса ДМС. ", font);
                        Paragraph p2 = new Paragraph("В соответствии с требованиями статьи 10 Федерального закона от 27.07.2006 «О персональных данных» " +
                            "№ 152 - ФЗ даю согласие на обработку моих персональных данных Оператором при условии, что их обработка осуществляется лицом, " +
                            "профессионально занимающимся медицинской деятельностью и обязанным сохранять врачебную тайну.", font);
                        Paragraph p3 = new Paragraph("Оператор имеет право:\n - при обработке моих персональных данных вносить их в реестры, базы данных " +
                            "автоматизированных информационных систем для формирования отчётных форм и иных сведений, предоставление которых регламентировано договорами " +
                            "или иными документами, определяющими взаимодействие Оператора со страховыми медицинскими организациями, медицинскими организациями, " +
                            "органами управления здравоохранения, иными организациями;\n - с целью выполнения своих обязательств, предусмотренных нормативными правовыми актами " +
                            "или договорами, на предоставление, передачу моих персональных данных иным организациям, при условии, что указанные предоставление передача будут " +
                            "осуществляться с использованием машинных носителей или по каналам связи с соблюдением мер, обеспечивающих защиту моих персональных " +
                            "данных от несанкционированного доступа, а также при условии, что их прием и обработка будут осуществляться лицом, обязанным сохранять профессиональную тайну.", font);
                        Paragraph p4 = new Paragraph("Даю согласие на то, что срок хранения моих персональных данных соответствует сроку хранения " +
                            "медицинской карты и составляет двадцать пять лет.По истечении указанного срока хранения моих персональных данных Оператор обязан уничтожить все мои персональные данные, включая все копии на машинных носителях информации.", font);
                        Paragraph p5 = new Paragraph("Передача моих персональных данных иным лицам или иное их разглашение может осуществляться только с моего письменного согласия.", font);
                        Paragraph p6 = new Paragraph("Я согласен(а) со следующими действиями с моими персональными данными:" +
                            "\n1.Обработка моих персональных данных в защищённых в установленном порядке автоматизированных информационных системах персональных данных пациентов;" +
                            "\n2.Обработка моих персональных данных, защищённых в установленном порядке, без использования средств автоматизации. " +
                            "Я оставляю за собой право отозвать свое согласие полностью или частично по моей инициативе на основании личного письменного заявления," +
                            " в т.ч.и в случае ставших мне известных фактов нарушения моих прав при обработке персональных данных. В случае получения моего письменного " +
                            "заявления об отзыве настоящего согласия на обработку персональных данных Оператор обязан прекратить их обработку", font);

                        Paragraph p7 = new Paragraph("\n\n________________________________________" + "            _____________\n" +
                            "                 (подпись, ФИО)                                                   (дата)\n", underlineFont);
                        p7.Alignment = Element.ALIGN_CENTER;

                        var p8 = new Paragraph("Согласие получено ___________________________", font);
                        var p9 = new Paragraph("                                                                                                (дата)", underlineFont);

                        var p10 = new Paragraph("Уполномоченный представитель медицинской организации ________________________________", font);
                        var p11 = new Paragraph("                                                                                                                                                           " +
                            "                                                               (подпись, ФИО)", underlineFont);

                        p1.FirstLineIndent = 30;
                        p2.FirstLineIndent = 30;
                        p3.FirstLineIndent = 30;
                        p4.FirstLineIndent = 30;
                        p5.FirstLineIndent = 30;
                        p6.FirstLineIndent = 30;

                        p2.Alignment = Element.ALIGN_JUSTIFIED;
                        p3.Alignment = Element.ALIGN_JUSTIFIED;
                        p4.Alignment = Element.ALIGN_JUSTIFIED;
                        p5.Alignment = Element.ALIGN_JUSTIFIED;
                        p6.Alignment = Element.ALIGN_JUSTIFIED;

                        document.Add(p);
                        document.Add(p1);
                        document.Add(p2);
                        document.Add(p3);
                        document.Add(p4);
                        document.Add(p5);
                        document.Add(p6);
                        document.Add(p7);
                        document.Add(p8);
                        document.Add(p9);
                        document.Add(p10);
                        document.Add(p11);

                        document.Close();
                        writer.Close();

                        System.Windows.MessageBox.Show("Документ сохранен по пути: " + pathToSave);
                    }
                }
            }

        }
    }
}
