// API Configuration
const API_BASE_URL = 'https://localhost:7234/api';

let invoiceToDelete = null;

// Load invoices on page load
document.addEventListener('DOMContentLoaded', function() {
    loadInvoices();
});

// Load all invoices
async function loadInvoices() {
    try {
        showLoadingMessage();

        const response = await fetch(`${API_BASE_URL}/invoices`);

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const invoices = await response.json();

        hideLoadingMessage();
        displayInvoices(invoices);

    } catch (error) {
        hideLoadingMessage();
        showError(`Failed to load invoices: ${error.message}`);
        console.error('Error loading invoices:', error);
    }
}

// Display invoices in table
function displayInvoices(invoices) {
    const tableBody = document.getElementById('invoiceTableBody');
    const tableCard = document.getElementById('invoiceTableCard');
    const noInvoicesMessage = document.getElementById('noInvoicesMessage');

    if (!invoices || invoices.length === 0) {
        tableCard.style.display = 'none';
        noInvoicesMessage.style.display = 'block';
        return;
    }

    tableCard.style.display = 'block';
    noInvoicesMessage.style.display = 'none';

    tableBody.innerHTML = '';

    invoices.forEach(invoice => {
        const row = document.createElement('tr');

        const statusClass = getStatusBadgeClass(invoice.status);
        const formattedDate = new Date(invoice.invoiceDate).toLocaleDateString();
        const formattedDueDate = invoice.dueDate ? new Date(invoice.dueDate).toLocaleDateString() : '-';
        const formattedAmount = formatCurrency(invoice.totalAmount);

        row.innerHTML = `
            <td><strong>${escapeHtml(invoice.invoiceNumber)}</strong></td>
            <td>${formattedDate}</td>
            <td>${escapeHtml(invoice.customerName)}</td>
            <td>${formattedAmount}</td>
            <td><span class="badge ${statusClass}">${invoice.status}</span></td>
            <td>${formattedDueDate}</td>
            <td>
                <button class="btn btn-sm btn-danger" onclick="showDeleteConfirmation(${invoice.id}, '${escapeHtml(invoice.invoiceNumber)}')">
                    <i class="bi bi-trash"></i> Delete
                </button>
            </td>
        `;

        tableBody.appendChild(row);
    });
}

// Show delete confirmation modal
function showDeleteConfirmation(invoiceId, invoiceNumber) {
    invoiceToDelete = invoiceId;
    document.getElementById('deleteInvoiceNumber').textContent = invoiceNumber;

    const modal = new bootstrap.Modal(document.getElementById('deleteModal'));
    modal.show();
}

// Confirm delete - attach to button click
document.addEventListener('DOMContentLoaded', function() {
    const confirmDeleteBtn = document.getElementById('confirmDeleteBtn');
    if (confirmDeleteBtn) {
        confirmDeleteBtn.addEventListener('click', async function() {
            if (invoiceToDelete) {
                await deleteInvoice(invoiceToDelete);

                // Hide modal
                const modal = bootstrap.Modal.getInstance(document.getElementById('deleteModal'));
                modal.hide();
            }
        });
    }
});

// Delete invoice
async function deleteInvoice(invoiceId) {
    try {
        const response = await fetch(`${API_BASE_URL}/invoices/${invoiceId}`, {
            method: 'DELETE'
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        showSuccess('Invoice deleted successfully!');

        // Reload invoices
        setTimeout(() => {
            loadInvoices();
        }, 1000);

    } catch (error) {
        showError(`Failed to delete invoice: ${error.message}`);
        console.error('Error deleting invoice:', error);
    }
}

// Helper functions
function getStatusBadgeClass(status) {
    switch(status) {
        case 'Draft': return 'bg-secondary';
        case 'Issued': return 'bg-primary';
        case 'Paid': return 'bg-success';
        default: return 'bg-secondary';
    }
}

function formatCurrency(amount) {
    return new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD'
    }).format(amount);
}

function escapeHtml(text) {
    const map = {
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;',
        '"': '&quot;',
        "'": '&#039;'
    };
    return text.replace(/[&<>"']/g, m => map[m]);
}

function showLoadingMessage() {
    document.getElementById('loadingMessage').style.display = 'block';
}

function hideLoadingMessage() {
    document.getElementById('loadingMessage').style.display = 'none';
}

function showError(message) {
    const errorDiv = document.getElementById('errorMessage');
    const errorText = document.getElementById('errorText');

    errorText.textContent = message;
    errorDiv.style.display = 'block';

    setTimeout(() => {
        errorDiv.style.display = 'none';
    }, 5000);
}

function showSuccess(message) {
    const successDiv = document.getElementById('successMessage');
    const successText = document.getElementById('successText');

    successText.textContent = message;
    successDiv.style.display = 'block';

    setTimeout(() => {
        successDiv.style.display = 'none';
    }, 3000);
}
