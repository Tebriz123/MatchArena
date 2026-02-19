// Load color data when page loads
document.addEventListener('DOMContentLoaded', function() {
    const urlParams = new URLSearchParams(window.location.search);
    const colorId = urlParams.get('id');
    
    if (colorId) {
        loadColorData(colorId);
    }
});

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

// Handle form submission
document.getElementById('updateColorForm').addEventListener('submit', function(e) {
    e.preventDefault();
    
    const formData = new FormData(this);
    
    const colorData = {
        id: document.getElementById('colorId').value,
        name: formData.get('colorName'),
        hex: formData.get('colorHex'),
        description: formData.get('description'),
        status: formData.get('status'),
        sortOrder: formData.get('sortOrder')
    };
    
    console.log('Updated Color Data:', colorData);
    
    alert('Rəng uğurla yeniləndi! ✓');
    window.location.href = 'admin-panel.html';
});

function loadColorData(colorId) {
    // Demo data for different colors
    const demoColors = {
        'CL001': {
            name: 'Qara',
            hex: '#000000',
            description: 'Klassik qara rəng',
            status: 'active',
            sortOrder: 1,
            productCount: 156
        },
        'CL002': {
            name: 'Ağ',
            hex: '#FFFFFF',
            description: 'Təmiz ağ rəng',
            status: 'active',
            sortOrder: 2,
            productCount: 142
        },
        'CL003': {
            name: 'Qırmızı',
            hex: '#FF0000',
            description: 'Parlaq qırmızı rəng',
            status: 'active',
            sortOrder: 3,
            productCount: 98
        },
        'CL004': {
            name: 'Mavi',
            hex: '#0000FF',
            description: 'Göy mavi rəng',
            status: 'active',
            sortOrder: 4,
            productCount: 87
        },
        'CL005': {
            name: 'Yaşıl',
            hex: '#00FF00',
            description: 'Parlaq yaşıl rəng',
            status: 'active',
            sortOrder: 5,
            productCount: 65
        },
        'CL006': {
            name: 'Sarı',
            hex: '#FFFF00',
            description: 'Parlaq sarı rəng',
            status: 'active',
            sortOrder: 6,
            productCount: 43
        }
    };

    const color = demoColors[colorId] || demoColors['CL001'];
    
    // Fill form fields
    document.getElementById('colorId').value = colorId;
    document.getElementById('colorName').value = color.name;
    document.getElementById('colorHex').value = color.hex;
    document.getElementById('colorPicker').value = color.hex;
    document.getElementById('description').value = color.description || '';
    document.getElementById('status').value = color.status;
    document.getElementById('sortOrder').value = color.sortOrder;
    document.getElementById('productCount').textContent = color.productCount;
    
    // Update preview
    updateColorPreview(color.hex);
}
