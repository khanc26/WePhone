document.querySelectorAll('.quantity button').forEach(function (button) {
    button.addEventListener('click', function () {
        var oldValueElement = this.parentElement.parentElement.querySelector('input');
        var oldValue = parseFloat(oldValueElement.value);

        if (this.classList.contains('btn-plus')) {
            var newVal = oldValue + 1;
        } else {
            if (oldValue > 0) {
                var newVal = oldValue - 1;
            } else {
                newVal = 0;
            }
        }

        oldValueElement.value = newVal;
    });
});