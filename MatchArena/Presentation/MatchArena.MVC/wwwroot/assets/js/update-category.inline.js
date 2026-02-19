// Load category data when page loads
document.addEventListener('DOMContentLoaded', function() {
    const urlParams = new URLSearchParams(window.location.search);
    const categoryId = urlParams.get('id');
    
    if (categoryId) {
        loadCategoryData(categoryId);
    }
});

// Icon preview
document.getElementById('categoryIcon').addEventListener('input', function(e) {
    const icon = e.target.value || '📂';
    document.getElementById('iconPreview').textContent = icon;
});

// Auto-generate slug from category name (only if not manually edited)
document.getElementById('categoryName').addEventListener('input', function(e) {
    const slugInput = document.getElementById('slug');
    if (slugInput.dataset.autoGenerate !== 'false') {
        const slug = e.target.value
            .toLowerCase()
            .replace(/ə/g, 'e')
            .replace(/ı/g, 'i')
            .replace(/ğ/g, 'g')
            .replace(/ü/g, 'u')
            .replace(/ö/g, 'o')
            .replace(/ş/g, 's')
            .replace(/ç/g, 'c')
            .replace(/[^a-z0-9]+/g, '-')
            .replace(/^-+|-+$/g, '');
        slugInput.value = slug;
    }
});

// Prevent auto-generation if user manually edits slug
document.getElementById('slug').addEventListener('input', function() {
    this.dataset.autoGenerate = 'false';
});

// Handle form submission
document.getElementById('updateCategoryForm').addEventListener('submit', function(e) {
    e.preventDefault();
    
    const formData = new FormData(this);
    
    const categoryData = {
        id: document.getElementById('categoryId').value,
        name: formData.get('categoryName'),
        icon: formData.get('categoryIcon'),
        description: formData.get('description'),
        status: formData.get('status'),
        sortOrder: formData.get('sortOrder'),
        showInMenu: document.getElementById('showInMenu').checked,
        featured: document.getElementById('featured').checked,
        slug: formData.get('slug'),
        metaTitle: formData.get('metaTitle'),
        metaDescription: formData.get('metaDescription')
    };
    
    console.log('Updated Category Data:', categoryData);
    
    alert('Kateqoriya uğurla yeniləndi! ✓');
    window.location.href = 'admin-panel.html';
});

function loadCategoryData(categoryId) {
    // Demo data for different categories
    const demoCategories = {
        'C001': {
            name: 'Avadanlıq',
            icon: '⚽',
            description: 'Futbol avadanlıqları və aksesuarları',
            status: 'active',
            sortOrder: 1,
            showInMenu: true,
            featured: true,
            slug: 'avadanliq',
            metaTitle: 'Futbol Avadanlıqları - MatchArena',
            metaDescription: 'Professional futbol avadanlıqları və aksesuarları',
            productCount: 45
        },
        'C002': {
            name: 'Geyim',
            icon: '👕',
            description: 'İdman geyimləri və formalar',
            status: 'active',
            sortOrder: 2,
            showInMenu: true,
            featured: true,
            slug: 'geyim',
            metaTitle: 'İdman Geyimləri - MatchArena',
            metaDescription: 'Keyfiyyətli futbol formaları və idman geyimləri',
            productCount: 78
        },
        'C003': {
            name: 'Qoruyucu',
            icon: '🛡️',
            description: 'Təhlükəsizlik və qoruyucu avadanlıqlar',
            status: 'active',
            sortOrder: 3,
            showInMenu: true,
            featured: false,
            slug: 'qoruyucu',
            metaTitle: 'Qoruyucu Avadanlıqlar - MatchArena',
            metaDescription: 'Maksimum təhlükəsizlik üçün qoruyucu avadanlıqlar',
            productCount: 23
        },
        'C004': {
            name: 'Aksesuar',
            icon: '🎒',
            description: 'İdman çantaları və əlavə aksesuarlar',
            status: 'active',
            sortOrder: 4,
            showInMenu: true,
            featured: false,
            slug: 'aksesuar',
            metaTitle: 'İdman Aksesuarları - MatchArena',
            metaDescription: 'Futbolçular üçün faydalı aksesuar və avadanlıqlar',
            productCount: 12
        }
    };

    const category = demoCategories[categoryId] || demoCategories['C001'];
    
    // Fill form fields
    document.getElementById('categoryId').value = categoryId;
    document.getElementById('categoryName').value = category.name;
    document.getElementById('categoryIcon').value = category.icon;
    document.getElementById('iconPreview').textContent = category.icon;
    document.getElementById('description').value = category.description || '';
    document.getElementById('status').value = category.status;
    document.getElementById('sortOrder').value = category.sortOrder;
    document.getElementById('showInMenu').checked = category.showInMenu;
    document.getElementById('featured').checked = category.featured;
    document.getElementById('slug').value = category.slug;
    document.getElementById('slug').dataset.autoGenerate = 'false';
    document.getElementById('productCount').textContent = category.productCount;
    
    if (category.metaTitle) document.getElementById('metaTitle').value = category.metaTitle;
    if (category.metaDescription) document.getElementById('metaDescription').value = category.metaDescription;
}
