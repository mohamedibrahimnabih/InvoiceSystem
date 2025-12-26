// API Configuration
const API_BASE_URL = 'https://localhost:7234/api';

let itemCounter = 0;
const items = [];

// Initialize on page load
document.addEventListener('DOMContentLoaded', function() {
    // Set today's date as default
    const today = new Date().toISOString().split('T')[0];
    document.getElementById('invoiceDate').value = today;

    // Add first two items by default
    addItem();
    addItem();

    // Event listeners
    document.getElementById('addItemBtn').addEventListener('click', addItem);
    document.getElementById('createInvoiceForm').addEventListener('submit', handleSubmit);
});

// Add new invoice item
function addItem() {
    const itemId = itemCounter++;
    const container = document.getElementById('itemsContainer');
    const noItemsMessage = document.getElementById('noItemsMessage');

    noItemsMessage.style.display = 'none';

    const itemCard = document.createElement('div');
    itemCard.className = 'card mb-3';
    itemCard.id = `item-${itemId}`;

    itemCard.innerHTML = `
        <div class="card-header d-flex justify-content-between align-items-center">
            <h6 class="mb-0">Item #${items.length + 1}</h6>
            <button type="button" class="btn btn-sm btn-danger" onclick="removeItem(${itemId})">
                <i class="bi bi-trash"></i> Remove
            </button>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-md-6 mb-3">
                    <label for="productName-${itemId}" class="form-label">Product Name *</label>
                    <input type="text" class="form-control" id="productName-${itemId}" required>
                </div>
                <div class="col-md-6 mb-3">
                    <label for="description-${itemId}" class="form-label">Description</label>
                    <input type="text" class="form-control" id="description-${itemId}">
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 mb-3">
                    <label for="quantity-${itemId}" class="form-label">Quantity *</label>
                    <input type="number" class="form-control" id="quantity-${itemId}" min="1" value="1" required>
                </div>
                <div class="col-md-3 mb-3">
                    <label for="unitPrice-${itemId}" class="form-label">Unit Price *</label>
                    <input type="number" class="form-control" id="unitPrice-${itemId}" min="0" step="0.01" value="0" required>
                </div>
                <div class="col-md-3 mb-3">
                    <label for="taxRate-${itemId}" class="form-label">Tax Rate (%)</label>
                    <input type="number" class="form-control" id="taxRate-${itemId}" min="0" max="100" step="0.01" value="0">
                </div>
                <div class="col-md-3 mb-3">
                    <label for="discount-${itemId}" class="form-label">Discount (%)</label>
                    <input type="number" class="form-control" id="discount-${itemId}" min="0" max="100" step="0.01" value="0">
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <strong>Line Total: <span id="lineTotal-${itemId}" class="text-primary">$0.00</span></strong>
                </div>
            </div>
        </div>
    `;

    container.appendChild(itemCard);
    items.push(itemId);

    // Add event listeners for calculation
    ['quantity', 'unitPrice', 'taxRate', 'discount'].forEach(field => {
        document.getElementById(`${field}-${itemId}`).addEventListener('input', () => calculateTotals());
    });

    calculateTotals();
}

// Remove invoice item
function removeItem(itemId) {
    if (items.length <= 1) {
        alert('At least one item is required!');
        return;
    }

    const itemCard = document.getElementById(`item-${itemId}`);
    if (itemCard) {
        itemCard.remove();
        const index = items.indexOf(itemId);
        if (index > -1) {
            items.splice(index, 1);
        }
    }

    // Update item numbers
    updateItemNumbers();
    calculateTotals();

    if (items.length === 0) {
        document.getElementById('noItemsMessage').style.display = 'block';
    }
}

// Update item numbers after removal
function updateItemNumbers() {
    items.forEach((itemId, index) => {
        const header = document.querySelector(`#item-${itemId} .card-header h6`);
        if (header) {
            header.textContent = `Item #${index + 1}`;
        }
    });
}

// Calculate totals
function calculateTotals() {
    let subtotal = 0;
    let totalTax = 0;

    items.forEach(itemId => {
        const quantity = parseFloat(document.getElementById(`quantity-${itemId}`)?.value || 0);
        const unitPrice = parseFloat(document.getElementById(`unitPrice-${itemId}`)?.value || 0);
        const taxRate = parseFloat(document.getElementById(`taxRate-${itemId}`)?.value || 0);
        const discount = parseFloat(document.getElementById(`discount-${itemId}`)?.value || 0);

        // Calculate line total
        const itemSubtotal = quantity * unitPrice;
        const discountAmount = itemSubtotal * (discount / 100);
        const lineTotal = itemSubtotal - discountAmount;
        const lineTax = lineTotal * (taxRate / 100);

        // Update line total display
        const lineTotalElement = document.getElementById(`lineTotal-${itemId}`);
        if (lineTotalElement) {
            lineTotalElement.textContent = formatCurrency(lineTotal);
        }

        subtotal += lineTotal;
        totalTax += lineTax;
    });

    const total = subtotal + totalTax;

    // Update totals display
    document.getElementById('subtotalAmount').textContent = formatCurrency(subtotal);
    document.getElementById('taxAmount').textContent = formatCurrency(totalTax);
    document.getElementById('totalAmount').textContent = formatCurrency(total);
}

// Handle form submission
async function handleSubmit(e) {
    e.preventDefault();

    if (items.length === 0) {
        showError('At least one item is required!');
        return;
    }

    // Collect form data
    const invoiceData = {
        invoiceNumber: document.getElementById('invoiceNumber').value,
        invoiceDate: document.getElementById('invoiceDate').value,
        customerName: document.getElementById('customerName').value,
        dueDate: document.getElementById('dueDate').value || null,
        notes: document.getElementById('notes').value || null,
        items: []
    };

    // Collect item data
    items.forEach(itemId => {
        const item = {
            productName: document.getElementById(`productName-${itemId}`).value,
            quantity: parseInt(document.getElementById(`quantity-${itemId}`).value),
            unitPrice: parseFloat(document.getElementById(`unitPrice-${itemId}`).value),
            taxRate: parseFloat(document.getElementById(`taxRate-${itemId}`).value || 0),
            discountPercentage: parseFloat(document.getElementById(`discount-${itemId}`).value || 0),
            description: document.getElementById(`description-${itemId}`).value || null
        };

        invoiceData.items.push(item);
    });

    // Submit to API
    try {
        const submitBtn = document.getElementById('submitBtn');
        submitBtn.disabled = true;
        submitBtn.innerHTML = '<i class="bi bi-hourglass-split"></i> Creating...';

        const response = await fetch(`${API_BASE_URL}/invoices`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(invoiceData)
        });

        if (!response.ok) {
            const errorData = await response.text();
            throw new Error(errorData || `HTTP error! status: ${response.status}`);
        }

        const invoiceId = await response.json();

        // Show success and redirect
        alert(`Invoice created successfully! Invoice ID: ${invoiceId}`);
        window.location.href = '/Invoices/Index';

    } catch (error) {
        showError(`Failed to create invoice: ${error.message}`);
        console.error('Error creating invoice:', error);

        const submitBtn = document.getElementById('submitBtn');
        submitBtn.disabled = false;
        submitBtn.innerHTML = '<i class="bi bi-save"></i> Create Invoice';
    }
}

// Helper functions
function formatCurrency(amount) {
    return new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD'
    }).format(amount);
}

function showError(message) {
    const errorDiv = document.getElementById('errorMessage');
    const errorText = document.getElementById('errorText');

    errorText.textContent = message;
    errorDiv.style.display = 'block';

    window.scrollTo({ top: 0, behavior: 'smooth' });

    setTimeout(() => {
        errorDiv.style.display = 'none';
    }, 5000);
}
