using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RIFDC;
using RIFDC.CommonFunctions;
using RIFDC.CommonFunctions.Logger;

namespace PaymentServiceDemo_OtusHomeWork_Lecture14
{
    class Program
    {
        static void Main(string[] args)
        {
            PaymentServiceDemoApp.Run();
        }
    }
    public static class PaymentServiceDemoApp
    {
        public static IDataRoom MainDataRoom = Repository.GetFinAppDataRoom();

        public static void Run()
        {
            InitApp();

            string clientReply = "";

            do
            {
                ShowMenu();
                clientReply = Console.ReadLine().ToString();

                switch (clientReply)
                {
                    case "1":
                        FillTestData();
                        break;

                    case "2":
                        ShowTableContents();
                        break;

                    case "3":
                        AddClient();
                        break;

                    case "4":
                        AddTransaction();
                        break;

                    case "5":
                        AddContract();
                        break;

                    case "6":

                        break;

                    case "9":

                        break;
                }

                if (clientReply == "9")
                {
                    break;
                }

            }
            while (true);

            Console.WriteLine("Работа программы завершена. Нажмите любуй клавишу для выхода.");

            Console.ReadKey();

            MainDataRoom.disconnect();

            return;

        }

        static void Say(string s)
        {
            Console.WriteLine(s);
        }
        static void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("");
            Console.WriteLine("Выберите пункт меню");
            Console.WriteLine("1 -- Заполнить тестовые данные");
            Console.WriteLine("2 -- Показать содержимое таблиц");
            Console.WriteLine("-------------------");
            Console.WriteLine("3 -- Добавить клиента");
            Console.WriteLine("4 -- Добавить транзакцию");
            Console.WriteLine("5 -- Добавить контракт");
            Console.WriteLine("-------------------");
            Console.WriteLine("9 -- Выход");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
        }


        public static void InitApp()
        {
            //Logger.logDirection = Logger.logDirectionEnum.bothToConAndFile;
            // Logger.fileName = @"\..\..\Logs.txt";

            Console.WriteLine("Инициализация приложения");
            Logger.logDirection = LogDirectionEnum.bothToConAndFile;

            Logger.fileName = @"..\..\..\PaymentServiceLogs.txt";
            Logger.prepare();
            
            /*
            
            MySqlCluster.connectionData.server = "37.140.192.97";
            //cls_mysql.connectionData.port = "3306";
            MySqlCluster.connectionData.dbName = "u1325524_paymentservicedemo";
            MySqlCluster.connectionData.dbUser = "u1325524_paymentservicedemo_user01";
            MySqlCluster.connectionData.dbPassword = "2qT5!d6i";

            RIFDC_App.mainDataRoom = MainDataRoom;
            RIFDC_App.currentUserId = "user01";

            MainDataRoom.actualCluster = MySqlCluster;

            */

            Lib.DbOperationResult or = MainDataRoom.connect();

            if (!or.success)
            {
                Console.WriteLine("Ошибка подключения, программа остановлена");
                Console.WriteLine(or.msg);
                return;
            }
            Console.WriteLine("Инициализация БД успешна");
        }

        public static void FillTestData()
        {
            Console.WriteLine("");
            Console.WriteLine("Заполняем тестовые данные");
            Console.WriteLine("Мерчанты");

            ItemKeeper<Merchamt> merchants = ItemKeeper<Merchamt>.getInstance(MainDataRoom, true);

            if (!merchants.myStatusIsOk) return;

            IKeepable merch1;

            merch1 = merchants.createNewObject_inserted();
            merch1.setMyParameter("FullName", "ООО Рога и Копыта");
            merch1.setMyParameter("INN", "12345678925888");
            merch1.setMyParameter("JurAddress", "Одесса, Дерибасовская, дом 1");
            merch1.setMyParameter("Description", "Фирма создана Остапом Бендером");
            merchants.saveItem(merch1);

            merch1 = merchants.createNewObject_inserted();
            merch1.setMyParameter("FullName", "ООО Магазин Пятерочка");
            merch1.setMyParameter("INN", "4543186966514");
            merch1.setMyParameter("JurAddress", "Поселок Первомайское, ул. Советская, 4");
            merch1.setMyParameter("Description", "Это наша Пятерка");
            merchants.saveItem(merch1);

            merch1 = merchants.createNewObject_inserted();
            merch1.setMyParameter("FullName", "ООО Леруа Мерлен СПб");
            merch1.setMyParameter("INN", "890328903248");
            merch1.setMyParameter("JurAddress", "г.Санкт-Петербург, проспект Испытателей, 5");
            merch1.setMyParameter("Description", "Это Леруа, в который мы все время ездим");
            merchants.saveItem(merch1);

            merch1 = merchants.createNewObject_inserted();
            merch1.setMyParameter("FullName", "ООО Бутик Картин СПб");
            merch1.setMyParameter("INN", "55908098908");
            merch1.setMyParameter("JurAddress", "г.Москва, ул. Домостроительная, д.8 строение 2");
            merch1.setMyParameter("Description", "Это люди, которые продают картины через интернет");
            merchants.saveItem(merch1);

            merch1 = merchants.createNewObject_inserted();
            merch1.setMyParameter("FullName", "ООО Вимос");
            merch1.setMyParameter("INN", "2132188521");
            merch1.setMyParameter("JurAddress", "г.Рощино, ул. Пионерская 24");
            merch1.setMyParameter("Description", "В этом магазине я покупал косилку");
            merchants.saveItem(merch1);

            Console.WriteLine("");
            Console.WriteLine("Договоры");

            ItemKeeper<Contract> contracts = ItemKeeper<Contract>.getInstance(MainDataRoom, true);
            if (!contracts.myStatusIsOk) return;
            IKeepable contract1;

            foreach (IKeepable x in merchants.items)
            {
                contract1 = contracts.createNewObject_inserted();
                contract1.setMyParameter("merchantId", x.id);
                contract1.setMyParameter("contractNumber", "Contract-" + fn.generate4blockGUID().Substring(0, 4));
                contract1.setMyParameter("contractSignDate", DateTime.Now);
                contract1.setMyParameter("contractExiryDate", DateTime.Now);
                contracts.saveItem(contract1);
            }

            Console.WriteLine("");
            Console.WriteLine("Транзакции");
            ItemKeeper<Transaction> transactions = ItemKeeper<Transaction>.getInstance(MainDataRoom, true);
            if (!transactions.myStatusIsOk) return;
            IKeepable transaction1;

            for (int i = 0; i < 10; i++)
            {
                transaction1 = transactions.createNewObject_inserted();
                transaction1.setMyParameter("merchantId", merchants.GetRandomObject().id);
                transaction1.setMyParameter("amount", fn.getRandomDouble(0, 1000));
                transaction1.setMyParameter("operationTypeId", fn.getRandomWord(new string[] { "Payment" }));
                transaction1.setMyParameter("paymentMethodId", fn.getRandomWord(new string[] { "CardPayment", "PaymentSystem", "SberbankOnline" }));
                transactions.saveItem(transaction1);
            }
        }

        public static void ShowTableContents()
        {
            //показать содержимое таблиц
            Console.WriteLine("");
            Console.WriteLine("Клиенты");
            ItemKeeper<Merchamt> merchants = ItemKeeper<Merchamt>.getInstance(MainDataRoom, false);
            merchants.readItems();
            merchants.simpleObjectDump();

            Console.WriteLine("");
            Console.WriteLine("Контракты");
            ItemKeeper<Contract> contracts = ItemKeeper<Contract>.getInstance(MainDataRoom, false);
            contracts.readItems();
            contracts.simpleObjectDump();

            Console.WriteLine("");
            Console.WriteLine("Транзакции");
            ItemKeeper<Transaction> transactions = ItemKeeper<Transaction>.getInstance(MainDataRoom, false);
            transactions.readItems();
            transactions.simpleObjectDump();


        }


        public static void AddClient()
        {

            Console.WriteLine("");
            Console.WriteLine("Добавляем клиента");

            ItemKeeper<Merchamt> merchants = ItemKeeper<Merchamt>.getInstance(MainDataRoom);

            if (!merchants.myStatusIsOk) return;

            IKeepable merch1;

            merch1 = merchants.createNewObject_inserted();
            merch1.setMyParameter("FullName", "ООО Рога и Копыта");
            merch1.setMyParameter("INN", "12345678925888");
            merch1.setMyParameter("JurAddress", "Одесса, Дерибасовская, дом 1");
            merch1.setMyParameter("Description", "Фирма создана Остапом Бендером");
            merchants.saveItem(merch1);
            Console.WriteLine("Клиент добавлен");
            Console.WriteLine("");

        }


        public static void AddContract()
        {

            Console.WriteLine("");
            Console.WriteLine("Добавляем контракт");

            ItemKeeper<Contract> contracts = ItemKeeper<Contract>.getInstance(MainDataRoom);
            if (!contracts.myStatusIsOk) return;
            IKeepable contract1;

            contract1 = contracts.createNewObject_inserted();
            contract1.setMyParameter("merchantId", "Merchant-ZZZ-ZZZZ-ZZZZ-ZZZZ");
            contract1.setMyParameter("contractNumber", "Contract-" + fn.generate4blockGUID().Substring(0, 4));
            contract1.setMyParameter("contractSignDate", DateTime.Now);
            contract1.setMyParameter("contractExiryDate", DateTime.Now);
            contracts.saveItem(contract1);
            Console.WriteLine("Контракт добавлен");
            Console.WriteLine("");
        }

        public static void AddTransaction()
        {
            Console.WriteLine("");
            Console.WriteLine("Добавляем транзакцию");

            ItemKeeper<Transaction> transactions = ItemKeeper<Transaction>.getInstance(MainDataRoom);
            if (!transactions.myStatusIsOk) return;
            IKeepable transaction1;

            transaction1 = transactions.createNewObject_inserted();
            transaction1.setMyParameter("merchantId", "Merchant-ZZZ-ZZZZ-ZZZZ-ZZZZ");
            transaction1.setMyParameter("amount", fn.getRandomDouble(0, 1000));
            transaction1.setMyParameter("operationTypeId", fn.getRandomWord(new string[] { "Payment" }));
            transaction1.setMyParameter("paymentMethodId", fn.getRandomWord(new string[] { "CardPayment", "PaymentSystem", "SberbankOnline" }));
            transactions.saveItem(transaction1);
            Console.WriteLine("Транзакция добавлена");
            Console.WriteLine("");
        }
    }
}
