document.getElementById('solveButton').addEventListener('click', () => {
    const inequality = document.getElementById('inequalityInput').value;
    const resultMessage = document.getElementById('resultMessage');

    try {
        const solution = solveInequality(inequality);
        resultMessage.textContent = `Giải: ${solution}`;
    } catch (error) {
        resultMessage.textContent = 'Bất phương trình không hợp lệ. Vui lòng thử lại.';
    }
});

function solveInequality(inequality) {
    // Kiểm tra dạng bất phương trình
    const regex = /([+-]?\d*\.?\d*)x\s*([<>]=?)\s*([+-]?\d*\.?\d*)/;
    const match = inequality.match(regex);

    if (!match) throw new Error('Định dạng không hợp lệ');

    const a = parseFloat(match[1]) || 1; // Hệ số x
    const operator = match[2]; // Toán tử
    const b = parseFloat(match[3]); // Hằng số bên phải

    // Giải bất phương trình
    let solution;

    if (a === 0) {
        if (b >= 0) {
            solution = 'Tất cả x thỏa mãn';
        } else {
            solution = 'Không có nghiệm';
        }
    } else {
        const x = b / a;
        solution = `x ${operator} ${x}`;
    }

    return solution;
}