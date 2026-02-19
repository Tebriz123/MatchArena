// Load size data when page loads
document.addEventListener('DOMContentLoaded', function() {
    const urlParams = new URLSearchParams(window.location.search);
    const sizeId = urlParams.get('id');
    
    if (sizeId) {
        loadSizeData(sizeId);
    }
});

// Handle form submission
document.getElementById('updateSizeForm').addEventListener('submit', function(e) {
    e.preventDefault();
    
    const formData = new FormData(this);
    
    const sizeData = {
        id: document.getElementById('sizeId').value,
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
    
    console.log('Updated Size Data:', sizeData);
    
    alert('Ölçü uğurla yeniləndi! ✓');
    window.location.href = 'admin-panel.html';
});

function loadSizeData(sizeId) {
    // Demo data for different sizes
    const demoSizes = {
        'S001': {
            name: 'XS',
            code: 'Extra Small',
            category: 'clothing',
            status: 'active',
            description: 'Çox kiçik ölçü',
            sortOrder: 1,
            equivalentSize: 'EU 34, UK 6',
            productCount: 45,
            measurements: {
                chest: 84,
                waist: 68,
                height: 160
            }
        },
        'S002': {
            name: 'S',
            code: 'Small',
            category: 'clothing',
            status: 'active',
            description: 'Kiçik ölçü',
            sortOrder: 2,
            equivalentSize: 'EU 36, UK 8',
            productCount: 78,
            measurements: {
                chest: 88,
                waist: 72,
                height: 165
            }
        },
        'S003': {
            name: 'M',
            code: 'Medium',
            category: 'clothing',
            status: 'active',
            description: 'Orta ölçü',
            sortOrder: 3,
            equivalentSize: 'EU 38-40, UK 10-12',
            productCount: 112,
            measurements: {
                chest: 92,
                waist: 76,
                height: 170
            }
        },
        'S004': {
            name: 'L',
            code: 'Large',
            category: 'clothing',
            status: 'active',
            description: 'Böyük ölçü',
            sortOrder: 4,
            equivalentSize: 'EU 42-44, UK 14-16',
            productCount: 98,
            measurements: {
                chest: 98,
                waist: 82,
                height: 175
            }
        },
        'S005': {
            name: 'XL',
            code: 'Extra Large',
            category: 'clothing',
            status: 'active',
            description: 'Çox böyük ölçü',
            sortOrder: 5,
            equivalentSize: 'EU 46-48, UK 18-20',
            productCount: 67,
            measurements: {
                chest: 104,
                waist: 88,
                height: 180
            }
        },
        'S006': {
            name: '39',
            code: 'EU 39',
            category: 'shoes',
            status: 'active',
            description: 'Ayaqqabı ölçüsü 39',
            sortOrder: 6,
            equivalentSize: 'US 6, UK 6',
            productCount: 34,
            measurements: {}
        },
        'S007': {
            name: '42',
            code: 'EU 42',
            category: 'shoes',
            status: 'active',
            description: 'Ayaqqabı ölçüsü 42',
            sortOrder: 7,
            equivalentSize: 'US 8.5, UK 8',
            productCount: 56,
            measurements: {}
        },
        'S008': {
            name: 'One Size',
            code: 'Universal',
            category: 'universal',
            status: 'active',
            description: 'Universal ölçü, bütün ölçülərə uyğun',
            sortOrder: 8,
            equivalentSize: 'Hamıya uyğun',
            productCount: 89,
            measurements: {}
        }
    };

    const size = demoSizes[sizeId] || demoSizes['S003'];
    
    // Fill form fields
    document.getElementById('sizeId').value = sizeId;
    document.getElementById('sizeName').value = size.name;
    document.getElementById('sizeCode').value = size.code;
    document.getElementById('sizeCategory').value = size.category;
    document.getElementById('status').value = size.status;
    document.getElementById('description').value = size.description || '';
    document.getElementById('sortOrder').value = size.sortOrder;
    document.getElementById('equivalentSize').value = size.equivalentSize || '';
    document.getElementById('productCount').textContent = size.productCount;
    
    // Fill measurements if available
    if (size.measurements) {
        if (size.measurements.chest) document.getElementById('chestSize').value = size.measurements.chest;
        if (size.measurements.waist) document.getElementById('waistSize').value = size.measurements.waist;
        if (size.measurements.height) document.getElementById('heightSize').value = size.measurements.height;
    }
}
