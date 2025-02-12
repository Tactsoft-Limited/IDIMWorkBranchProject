// banglaNumberConverter.js

const words = [
    "শ\u09c2ন\u09cdয", "এক", "দ\u09c2ই", "ত\u09bfন", "চ\u09beর", "প\u09be\u0981চ", "ছয়", "স\u09beত", "আট", "নয়",
    "দশ", "এগ\u09beর\u09cb", "ব\u09beর\u09cb", "ত\u09c7র\u09cb", "চ\u09ccদ\u09cdদ", "পন\u09c7র\u09cb", "ষ\u09cbল", "সত\u09c7র\u09cb", "আঠ\u09beর\u09cb", "উন\u09bfশ",
    "ব\u09bfশ", "এক\u09c1শ", "ব\u09beইশ", "ত\u09c7ইশ", "চব\u09cdব\u09bfশ", "প\u0981চ\u09bfশ", "ছ\u09beব\u09cdব\u09bfশ", "স\u09beত\u09beশ", "আঠ\u09beশ", "ঊনত\u09cdর\u09bfশ",
    "ত\u09cdর\u09bfশ", "একত\u09cdর\u09bfশ", "বত\u09cdর\u09bfশ", "ত\u09c7ত\u09cdর\u09bfশ", "চ\u09ccত\u09cdর\u09bfশ", "প\u0981য\u09bcত\u09cdর\u09bfশ", "ছত\u09cdর\u09bfশ", "স\u09be\u0981ইত\u09cdর\u09bfশ", "আটত\u09cdর\u09bfশ", "ঊনচল\u09cdল\u09bfশ",
    "চল\u09cdল\u09bfশ", "একচল\u09cdল\u09bfশ", "ব\u09bfয\u09bc\u09beল\u09cdল\u09bfশ", "ত\u09c7ত\u09beল\u09cdল\u09bfশ", "চ\u09c1য\u09bc\u09beল\u09cdল\u09bfশ", "প\u0981য\u09bcত\u09beল\u09cdল\u09bfশ", "ছ\u09c7চল\u09cdল\u09bfশ", "স\u09beতচল\u09cdল\u09bfশ", "আটচল\u09cdল\u09bfশ", "ঊনপঞ\u09cdচ\u09beশ",
    "পঞ\u09cdচ\u09beশ", "এক\u09beন\u09cdন", "ব\u09beহ\u09beন\u09cdন", "ত\u09bfপ\u09cdপ\u09beন\u09cdন", "চ\u09c1য\u09bc\u09beন\u09cdন", "পঞ\u09cdচ\u09beন\u09cdন", "ছ\u09beপ\u09cdপ\u09beন\u09cdন", "স\u09beত\u09beন\u09cdন", "আট\u09beন\u09cdন", "ঊনষ\u09beট",
    "ষ\u09beট", "একষট\u09cdট\u09bf", "ব\u09beষট\u09cdট\u09bf", "ত\u09c7ষট\u09cdট\u09bf", "চ\u09ccষট\u09cdট\u09bf", "প\u0981য\u09bcষট\u09cdট\u09bf", "ছ\u09c7ষট\u09cdট\u09bf", "স\u09beতষট\u09cdট\u09bf", "আটষট\u09cdট\u09bf", "ঊনসত\u09cdতর",
    "সত\u09cdতর", "এক\u09beত\u09cdতর", "ব\u09beহ\u09beত\u09cdতর", "ত\u09bfয\u09bc\u09beত\u09cdতর", "চ\u09c1য\u09bc\u09beত\u09cdতর", "প\u0981চ\u09beত\u09cdতর", "ছ\u09bfয\u09bc\u09beত\u09cdতর", "স\u09beত\u09beত\u09cdতর", "আট\u09beত\u09cdতর", "ঊনআশ\u09bf",
    "আশ\u09bf", "এক\u09beশ\u09bf", "ব\u09bfর\u09beশ\u09bf", "ত\u09bfর\u09beশ\u09bf", "চ\u09c1র\u09baশ\u09bf", "প\u0981চ\u09beশ\u09bf", "ছ\u09bfয\u09bc\u09beশ\u09bf", "স\u09beত\u09beশ\u09bf", "আট\u09beশ\u09bf", "ঊননব\u09cdবই",
    "নব\u09cdবই", "এক\u09beনব\u09cdবই", "ব\u09bfর\u09beনব\u09cdবই", "ত\u09bfর\u09beনব\u09cdবই", "চ\u09c1র\u09baন\u09cdবই", "প\u0981চ\u09beন\u09cdবই", "ছ\u09bfয\u09bc\u09beন\u09cdবই", "স\u09beত\u09beন\u09cdবই", "আট\u09beন\u09cdবই", "ন\u09bfর\u09beনব\u09cdবই"
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
    let text = '';
    let num2 = Math.floor(num / 10000000);
    if (num2 !== 0) {
        text += (num2 <= 99) ? words[num2] + " ক\u09cbট\u09bf " : toWord(num2) + " ক\u09cbট\u09bf ";
    }

    let num3 = num % 10000000;
    let num4 = Math.floor(num3 / 100000);
    if (num4 > 0) {
        text += words[num4] + " লক\u09cdষ ";
    }

    let num5 = num3 % 100000;
    let num6 = Math.floor(num5 / 1000);
    if (num6 > 0) {
        text += words[num6] + " হ\u09beজ\u09beর ";
    }

    let num7 = num5 % 1000;
    let num8 = Math.floor(num7 / 100);
    if (num8 > 0) {
        text += words[num8] + " শত ";
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
        text += " দশম\u09bfক ";
        text += toWord(num2);
    }

    return text;
}

function getTakaInWord(number) {
    isValid(number);
    if (number === 0.0) {
        return "শ\u09c2ন\u09cdয ট\u09beক\u09be";
    }

    if (number.toString().includes('.')) {
        return convertFloatNumberToMoneyFormat(number);
    }

    return getBanglaWord(number) + " ট\u09beক\u09be ";
}

function convertFloatNumberToMoneyFormat(number) {
    const text = number.toFixed(2);
    const [integerPart, decimalPart] = text.split('.');
    let result = getBanglaWord(parseInt(integerPart)) + " ট\u09beক\u09be ";
    if (decimalPart) {
        result += getBanglaWord(parseInt(decimalPart)) + " পয়স\u09be";
    }
    return result;
}

function getCommaSeparateBanglaDigit(number) {
    isValid(number);
    return getBanglaDigits(number).replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function getBanglaMonthName(monthOfYear) {
    const months = [
        "জ\u09beন\u09c1য়\u09beর\u09bf", "ফ\u09c7ব\u09cdর\u09c1য়\u09beর\u09bf", "ম\u09beর\u09cdচ", "এপ\u09cdর\u09bfল",
        "ম\u09c7", "জ\u09c1ন", "জ\u09c1ল\u09beই", "আগস\u09cdট", "স\u09c7প\u09cdট\u09c7ম\u09cdবর", "অক\u09cdট\u09cbবর",
        "নভ\u09c7ম\u09cdবর", "ড\u09bfস\u09c7ম\u09cdবর"
    ];

    if (monthOfYear >= 1 && monthOfYear <= 12) {
        return months[monthOfYear - 1];
    }

    throw new Error("Month of year should be between 1 and 12");
}

function getBanglaDayName(dayOfWeek) {
    const days = [
        "শ\u09c2ক\u09cdরব\u09beর", "শন\u09bfব\u09beর", "রব\u09bfব\u09beর", "স\u09cbমব\u09beর", "মঙ\u09cdগলব\u09beর",
        "ব\u09c1ধব\u09beর", "ব\u09c3হস\u09cdপত\u09bfব\u09beর"
    ];

    if (dayOfWeek < 0 || dayOfWeek > 6) {
        throw new Error("Invalid day of week. Must be between 0 and 6.");
    }

    return days[dayOfWeek];
}
