// Update color preview
function updateColorPreview(color) {
    document.getElementById('colorPreviewLarge').style.background = color;
    document.getElementById('colorPreviewSmall').style.background = color;
    document.getElementById('colorPreviewMedium').style.background = color;
}

// Sync hex input with color picker
document.getElementById('colorHex').addEventListener('input', function(e) {
    let value = e.target.value;
    
    // Auto-add # if not present
    if (value && !value.startsWith('#')) {
        value = '#' + value;
        e.target.value = value;
    }
    
    // Validate and update
    if (/^#[0-9A-Fa-f]{6}$/.test(value)) {
        document.getElementById('colorPicker').value = value;
        updateColorPreview(value);
    }
});

// Sync color picker with hex input
document.getElementById('colorPicker').addEventListener('input', function(e) {
    const color = e.target.value.toUpperCase();
    document.getElementById('colorHex').value = color;
    updateColorPreview(color);
});

// Initialize preview
updateColorPreview('#000000');

// Handle form submission
document.getElementById('createColorForm').addEventListener('submit', function(e) {
    e.preventDefault();
    
    const formData = new FormData(this);
    
    const colorData = {
        name: formData.get('colorName'),
        hex: formData.get('colorHex'),
        description: formData.get('description'),
        status: formData.get('status'),
        sortOrder: formData.get('sortOrder')
    };
    
    console.log('Color Data:', colorData);
    
    alert('Rəng uğurla yaradıldı! ✓');
    window.location.href = 'admin-panel.html';
});
