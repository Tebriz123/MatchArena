// Handle form submission
document.getElementById('createSizeForm').addEventListener('submit', function(e) {
    e.preventDefault();
    
    const formData = new FormData(this);
    
    const sizeData = {
        name: formData.get('sizeName'),
        code: formData.get('sizeCode'),
        category: formData.get('sizeCategory'),
        status: formData.get('status'),
        description: formData.get('description'),
        sortOrder: formData.get('sortOrder'),
        equivalentSize: formData.get('equivalentSize'),
        measurements: {
            chest: formData.get('chestSize'),
            waist: formData.get('waistSize'),
            height: formData.get('heightSize'),
            custom: formData.get('measurements')
        }
    };
    
    console.log('Size Data:', sizeData);
    
    alert('Ölçü uğurla yaradıldı! ✓');
    window.location.href = 'admin-panel.html';
});

// Auto-generate size code from size name if not filled
document.getElementById('sizeName').addEventListener('input', function(e) {
    const codeInput = document.getElementById('sizeCode');
    if (!codeInput.value) {
        const name = e.target.value.toUpperCase();
        const sizeMap = {
            'XS': 'Extra Small',
            'S': 'Small',
            'M': 'Medium',
            'L': 'Large',
            'XL': 'Extra Large',
            'XXL': '2X Large',
            'XXXL': '3X Large'
        };
        
        if (sizeMap[name]) {
            codeInput.value = sizeMap[name];
        } else if (/^\d+$/.test(name)) {
            // If it's a number, assume it's a shoe size
            codeInput.value = `EU ${name}`;
        }
    }
});
