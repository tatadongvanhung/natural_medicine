$(document).ready(function () {
    var home = new Home();
});

class Home {

    static BankingAccountInfo = [
        {
            PaymentID: 1,
            BankName: "Techcombank",
            AcountBank: 19033483813021,
            AcountName: "Pham Van Hieu",
            Address: "Techcombank Bac Tu Liem"
        },
        {
            PaymentID: 2,
            BankName: "BIDV",
            AcountBank: 21610000484004,
            AcountName: "Pham Van Hieu",
            Address: "BIDV Tay Ha Noi"
        },
        {
            PaymentID: 3,
            BankName: "Viettinbank",
            AcountBank: 100867387183,
            AcountName: "Pham Van Hieu",
            Address: "Viettinbank Tay Do"
        },
        {
            PaymentID: 4,
            BankName: "Vietcombank",
            AcountBank: 01235246574,
            AcountName: "Pham Van Hieu",
            Address: "Vietcombank Bac Tu Liem"
        },
        {
            PaymentID: 5,
            BankName: "VP Bank",
            AcountBank: 987654321,
            AcountName: "Pham Van Hieu",
            Address: "VPB Bac Tu Liem"
        },
        {
            PaymentID: 7,
            BankName: "Ví điện tử MOMO",
            AcountBank: 0123456789,
            AcountName: "Pham Van Hieu",
            Address: ""
        },
    ];

    constructor() {
        this.initEvents();
    }

    initEvents() {
        $('#payment_method_id').val('1');
        $('#payment_method_id').on('change', this.menuItemOnClick.bind(this));
        $('.paymentMethod').on('click', this.onClickBankItem.bind(this));
    }

    menuItemOnClick(event) {
        let $menuItem = $(event.target);

        if ($menuItem.val() == '1') {
            $('.paymentWrap').hide();
            $('.payment-info').hide();
        } else {
            $('.paymentWrap').show();
            $('.payment-info').show();
            $('#firstBank').click();

        }
    }

    onClickBankItem(event) {
        let $menuItem = $(event.target);

        let paymentid = $menuItem.closest("label").attr("paymentID");

        let curentBank = Home.BankingAccountInfo.find(x => x.PaymentID.toString() == paymentid);

        if (curentBank) {
            $("#paymentName").text(curentBank.BankName);
            $("#accountNumber").text(curentBank.AcountBank);
            $("#accountName").text(curentBank.AcountName);
            $("#addressBank").text(curentBank.Address);
        }
    }
}