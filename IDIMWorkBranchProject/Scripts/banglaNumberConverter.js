// banglaNumberConverter.js

const words = [
    "শূন্য", "এক", "দুই", "তিন", "চার", "পাঁচ", "ছয়", "সাত", "আট", "নয়",
    "দশ", "এগারো", "বারো", "তেরো", "চৌদ্দ", "পনেরো", "ষোল", "সতেরো", "আঠারো", "উনিশ",
    "বিশ", "একুশ", "বাইশ", "তেইশ", "চব্বিশ", "পঁচিশ", "ছাব্বিশ", "সাতাশ", "আঠাশ", "ঊনত্রিশ",
    "ত্রিশ", "একত্রিশ", "বত্রিশ", "তেত্রিশ", "চৌত্রিশ", "পঁয়ত্রিশ", "ছত্রিশ", "সাঁইত্রিশ", "আটত্রিশ", "ঊনচল্লিশ",
    "চল্লিশ", "একচল্লিশ", "বিয়াল্লিশ", "তেতাল্লিশ", "চুয়াল্লিশ", "পঁয়তাল্লিশ", "ছেচল্লিশ", "সাতচল্লিশ", "আটচল্লিশ", "ঊনপঞ্চাশ",
    "পঞ্চাশ", "একান্ন", "বাহান্ন", "তিপ্পান্ন", "চুয়ান্ন", "পঞ্চান্ন", "ছাপ্পান্ন", "সাতান্ন", "আটান্ন", "ঊনষাট",
    "ষাট", "একষট্টি", "বাষট্টি", "তেষট্টি", "চৌষট্টি", "পঁয়ষট্টি", "ছেষট্টি", "সাতষট্টি", "আটষট্টি", "ঊনসত্তর",
    "সত্তর", "একাত্তর", "বাহাত্তর", "তিয়াত্তর", "চুয়াত্তর", "পঁচাত্তর", "ছিয়াত্তর", "সাতাত্তর", "আটাত্তর", "ঊনআশি",
    "আশি", "একাশি", "বিরাশি", "তিরাশি", "চুরাশি", "পঁচাশি", "ছিয়াশি", "সাতাশি", "আটাশি", "ঊননব্বই",
    "নব্বই", "একানব্বই", "বিরানব্বই", "তিরানব্বই", "চুরানব্বই", "পঁচানব্বই", "ছিয়ানব্বই", "সাতানব্বই", "আটানব্বই", "নিরানব্বই"
];

function isValid(number) {
    if (isNaN(number) || /(\.\d+\.)|E\d+/.test(number.toString())) {
        throw new Error("Invalid Number");
    }

    if (Math.abs(Number(number)) > 999999999999999) {
        throw new Error("Number should be less than 999999999999999");
    }
}

function getBanglaDigits(number) {
    isValid(number);
    const dictionary = {
        '0': '০', '1': '১', '2': '২', '3': '৩', '4': '৪', '5': '৫', '6': '৬', '7': '৭', '8': '৮', '9': '৯'
    };

    let result = '';
    for (const c of number.toString()) {
        result += dictionary[c] || c;
    }

    return result;
}

function toWord(num) {
    if (num === 0) return 'শূন্য';

    let text = '';
    let num2 = Math.floor(num / 10000000);


    if (num2 !== 0) {
        text += (num2 <= 99) ? words[num2] + " কোটি " : toWord(num2) + " কোটি ";
    }

    let num3 = num % 10000000;
    let num4 = Math.floor(num3 / 100000);
    if (num4 > 0) {
        text += words[num4] + " লক্ষ ";
    }

    let num5 = num3 % 100000;
    let num6 = Math.floor(num5 / 1000);
    if (num6 > 0) {
        text += words[num6] + " হাজার ";
    }

    let num7 = num5 % 1000;
    let num8 = Math.floor(num7 / 100);
    if (num8 > 0) {
        text += words[num8] + "শত ";
    }

    let num9 = num7 % 100;
    if (num9 > 0) {
        text += words[num9];
    }

    return text;
}

function getBanglaWord(number) {
    isValid(number);
    const num = Math.floor(number);
    const num2 = Math.round((number - num) * 100);
    let text = toWord(num);
    if (num2 > 0) {
        text += " দশমিক ";
        text += toWord(num2);
    }

    return text;
}

function getTakaInWord(number) {
    isValid(number);
    if (number === 0.0) {
        return "শুণ্য টাকা";
    }

    if (number.toString().includes('.')) {
        return convertFloatNumberToMoneyFormat(number);
    }

    return getBanglaWord(number) + " টাকা ";
}

function convertFloatNumberToMoneyFormat(number) {
    const text = number.toFixed(2);
    const [integerPart, decimalPart] = text.split('.');
    let result = getBanglaWord(parseInt(integerPart)) + " টাকা ";
    if (decimalPart) {
        result += getBanglaWord(parseInt(decimalPart)) + " পয়সা ";
    }
    return result;
}

function getCommaSeparateBanglaDigit(number) {
    isValid(number);
    return getBanglaDigits(number).replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function getBanglaMonthName(monthOfYear) {
    const months = ["জানুয়ারি", "ফেব্রুয়ারি", "মার্চ", "এপ্রিল", "মে", "জুন", "জুলাই", "আগস্ট", "সেপ্টেম্বর", "অক্টোবর", "নভেম্বর", "ডিসেম্বর"];

    if (monthOfYear >= 1 && monthOfYear <= 12) {
        return months[monthOfYear - 1];
    }

    throw new Error("Month of year should be between 1 and 12");
}

function getBanglaDayName(dayOfWeek) {
    const days = ["শনিবার", "রবিবার", "সোমবার", "মঙ্গলবার", "বুধবার", "বৃহস্পতিবার", "শুক্রবার"];

    if (dayOfWeek < 0 || dayOfWeek > 6) {
        throw new Error("Invalid day of week. Must be between 0 and 6.");
    }

    return days[dayOfWeek];
}
