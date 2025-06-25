/**
 * Function to convert a given number into words.
 * @param {number} number - The number to be converted into words.
 * @returns {string} - The word representation of the given number.
 */

function getEnglishWord(number) {
    if (number < 0) return false;

    // Arrays to hold words for single-digit, double-digit, and below-hundred numbers
    const single_digit = ['', 'One', 'Two', 'Three', 'Four', 'Five', 'Six', 'Seven', 'Eight', 'Nine'];
    const double_digit = ['Ten', 'Eleven', 'Twelve', 'Thirteen', 'Fourteen', 'Fifteen', 'Sixteen', 'Seventeen', 'Eighteen', 'Nineteen'];
    const below_hundred = ['Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];

    // Handle whole part and decimal part
    let wholePart = Math.floor(number);
    const decimalPart = Math.round((number - wholePart) * 100);

    if (number === 0) return 'Zero';

    // Recursive function to translate the number into words
    function translate(number) {
        let word = "";

        if (number < 10) {
            word = single_digit[number];
        } else if (number < 20) {
            word = double_digit[number - 10];
        } else if (number < 100) {
            let rem = number % 10;
            word = below_hundred[Math.floor(number / 10) - 2] + (rem !== 0 ? ' ' + single_digit[rem] : '');
        } else if (number < 1000) {
            word = single_digit[Math.floor(number / 100)] + ' Hundred';
            if (number % 100 !== 0) {
                word += ' ' + translate(number % 100);
            }
        } else if (number < 100000) { // Thousand
            word = translate(Math.floor(number / 1000)) + ' Thousand';
            if (number % 1000 !== 0) {
                word += ' ' + translate(number % 1000);
            }
        } else if (number < 10000000) { // Lakh
            word = translate(Math.floor(number / 100000)) + ' Lakh';
            if (number % 100000 !== 0) {
                word += ' ' + translate(number % 100000);
            }
        } else { // Crore
            word = translate(Math.floor(number / 10000000)) + ' Crore';
            if (number % 10000000 !== 0) {
                word += ' ' + translate(number % 10000000);
            }
        }

        return word;
    }

    let wholeWords = translate(wholePart);
    const decimalWords = decimalPart > 0 ? ' And ' + translate(decimalPart) + ' Paise' : '';
    return wholeWords + decimalWords + ' Only.';
}