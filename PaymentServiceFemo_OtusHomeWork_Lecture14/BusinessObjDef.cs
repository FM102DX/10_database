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
        public string MerchantId;
        public string ContractNumber;
        public string ContractSignDate;
        public string ContractExiryDate;

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
                return ContractNumber;
            }
        }

        public override string getMyStringRepresentation()
        {
            return $"{id} {MerchantId} {ContractNumber} {ContractSignDate} {ContractExiryDate}";
        }

    }

    class Transaction : KeepableClass, IKeepable
    {
        public string MerchantId;
        public double Amount;
        public string OperationTypeId;
        public string PaymentMethodId;


        public override string tableName { get { return "Transactions"; } }
        public override string entityName { get { return "Transaction"; } }

        public override Lib.FieldsInfo _get_fieldsInfo()
        {
            Lib.FieldInfo x;
            Lib.FieldsInfo f = this.getInitialFieldsInfoObject();

            x = f.addFieldInfoObject("MechantId", "MechantId", Lib.FieldTypeEnum.String);
            x.nullabilityInfo.allowNull = false;
            x.nullabilityInfo.defaultValue = "NewMechantId";

            x = f.addFieldInfoObject("Amount", "Amount", Lib.FieldTypeEnum.Double);
            x.nullabilityInfo.allowNull = false;

            x = f.addFieldInfoObject("OperationTypeId", "OperationTypeId", Lib.FieldTypeEnum.String);
            x.nullabilityInfo.allowNull = false;

            x = f.addFieldInfoObject("PaymentMethodId", "PaymentMethodId", Lib.FieldTypeEnum.String);
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
            return $"{id} {MerchantId} {Amount} {OperationTypeId} {PaymentMethodId}";
        }

    }
}
