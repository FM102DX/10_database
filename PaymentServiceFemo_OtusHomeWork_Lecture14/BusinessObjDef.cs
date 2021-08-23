using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RIFDC;

namespace PaymentServiceDemo_OtusHomeWork_Lecture14
{
    class Merchamt : KeepableClass, IKeepable
    {
        public string FullName;
        public string INN;
        public string JurAddress;
        public string Description;

        public override string tableName { get { return "Merchants"; } }
        public override string entityName { get { return "Merchant"; } }

        public override Lib.FieldsInfo _get_fieldsInfo()
        {
            Lib.FieldInfo x;
            Lib.FieldsInfo f = this.getInitialFieldsInfoObject();

            x = f.addFieldInfoObject("FullName", "FullName", Lib.FieldTypeEnum.String);
            x.nullabilityInfo.allowNull = false;
            x.nullabilityInfo.defaultValue = "NewMerchant";
            x.caption = "Название";

            x = f.addFieldInfoObject("INN", "INN", Lib.FieldTypeEnum.String);
            x.nullabilityInfo.allowNull = true;

            x = f.addFieldInfoObject("description", "description", Lib.FieldTypeEnum.String);
            x.caption = "Комментарий";
            x.nullabilityInfo.allowNull = true;
            x.isSearchable = true;

            return f;
        }

        public override string displayName
        {
            get
            {
                return FullName;
            }
        }

        public override string getMyStringRepresentation()
        {
            return $"{id} {FullName} {INN} {JurAddress} {Description}";
        }

    }

    class Contract : KeepableClass, IKeepable
    {
        public string merchantId;
        public string contractNumber;
        public string contractSignDate;
        public string contractExiryDate;

        public override string tableName { get { return "Contracts"; } }
        public override string entityName { get { return "Contract"; } }

        public override Lib.FieldsInfo _get_fieldsInfo()
        {
            Lib.FieldInfo x;
            Lib.FieldsInfo f = this.getInitialFieldsInfoObject();

            x = f.addFieldInfoObject("merchantId", "merchantId", Lib.FieldTypeEnum.String);
            x.nullabilityInfo.allowNull = false;
            x.nullabilityInfo.defaultValue = "NewMerchantId";

            x = f.addFieldInfoObject("contractNumber", "contractNumber", Lib.FieldTypeEnum.String);
            x.nullabilityInfo.allowNull = true;
            x.caption = "Номер Контракта";

            x = f.addFieldInfoObject("contractSignDate", "contractSignDate", Lib.FieldTypeEnum.Date);
            x.caption = "Дата подписания контракта";
            x.nullabilityInfo.allowNull = false;

            x = f.addFieldInfoObject("contractExiryDate", "contractExiryDate", Lib.FieldTypeEnum.Date);
            x.caption = "Дата подписания контракта";
            x.nullabilityInfo.allowNull = true;



            return f;
        }

        public override string displayName
        {
            get
            {
                return contractNumber;
            }
        }

        public override string getMyStringRepresentation()
        {
            return $"{id} {merchantId} {contractNumber} {contractSignDate} {contractExiryDate}";
        }

    }

    class Transaction : KeepableClass, IKeepable
    {
        public string merchantId;
        public double amount;
        public string operationTypeId;
        public string paymentMethodId;


        public override string tableName { get { return "Transactions"; } }
        public override string entityName { get { return "Transaction"; } }

        public override Lib.FieldsInfo _get_fieldsInfo()
        {
            Lib.FieldInfo x;
            Lib.FieldsInfo f = this.getInitialFieldsInfoObject();

            x = f.addFieldInfoObject("mechantId", "mechantId", Lib.FieldTypeEnum.String);
            x.nullabilityInfo.allowNull = false;
            x.nullabilityInfo.defaultValue = "newMechantId";

            x = f.addFieldInfoObject("amount", "amount", Lib.FieldTypeEnum.Double);
            x.nullabilityInfo.allowNull = false;

            x = f.addFieldInfoObject("operationTypeId", "operationTypeId", Lib.FieldTypeEnum.String);
            x.nullabilityInfo.allowNull = false;

            x = f.addFieldInfoObject("paymentMethodId", "paymentMethodId", Lib.FieldTypeEnum.String);
            x.nullabilityInfo.allowNull = false;


            return f;
        }

        public override string displayName
        {
            get
            {
                return "operation-"+id;
            }
        }

        public override string getMyStringRepresentation()
        {
            return $"{id} {merchantId} {amount} {operationTypeId} {paymentMethodId}";
        }

    }
}
