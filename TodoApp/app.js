// Todo App - Local Storage Implementation

class TodoApp {
    constructor() {
        this.todos = [];
        this.filterCategory = 'all';
        this.filterPriority = 'all';
        this.isSorted = false;
        this.storageKey = 'todoAppData';
        
        this.init();
    }

    init() {
        this.loadFromStorage();
        this.attachEventListeners();
        this.render();
    }

    attachEventListeners() {
        // Add todo
        document.getElementById('addBtn').addEventListener('click', () => this.addTodo());
        document.getElementById('todoInput').addEventListener('keypress', (e) => {
            if (e.key === 'Enter') this.addTodo();
        });

        // Filters
        document.getElementById('categorySelect').addEventListener('change', (e) => {
            this.filterCategory = e.target.value;
            this.render();
        });

        document.getElementById('prioritySelect').addEventListener('change', (e) => {
            this.filterPriority = e.target.value;
            this.render();
        });

        // Action buttons
        document.getElementById('clearCompletedBtn').addEventListener('click', () => this.clearCompleted());
        document.getElementById('sortBtn').addEventListener('click', () => this.toggleSort());
        document.getElementById('exportBtn').addEventListener('click', () => this.exportData());
    }

    addTodo() {
        const input = document.getElementById('todoInput');
        const text = input.value.trim();

        if (!text) {
            this.showNotification('Please enter a task!');
            return;
        }

        const category = this.getCategoryFromInput();
        const priority = this.getPriorityFromInput();

        const todo = {
            id: Date.now(),
            text: text,
            completed: false,
            category: category,
            priority: priority,
            createdAt: new Date().toISOString(),
            dueDate: null
        };

        this.todos.push(todo);
        input.value = '';
        this.saveToStorage();
        this.render();
        this.showNotification('Task added successfully!');
    }

    getCategoryFromInput() {
        // Default to 'other', can be enhanced with a dropdown in input
        return 'other';
    }

    getPriorityFromInput() {
        // Default to 'medium', can be enhanced with a priority picker
        return 'medium';
    }

    toggleTodo(id) {
        const todo = this.todos.find(t => t.id === id);
        if (todo) {
            todo.completed = !todo.completed;
            this.saveToStorage();
            this.render();
        }
    }

    deleteTodo(id) {
        this.todos = this.todos.filter(t => t.id !== id);
        this.saveToStorage();
        this.render();
        this.showNotification('Task deleted!');
    }

    clearCompleted() {
        const completedCount = this.todos.filter(t => t.completed).length;
        if (completedCount === 0) {
            this.showNotification('No completed tasks to clear!');
            return;
        }

        this.todos = this.todos.filter(t => !t.completed);
        this.saveToStorage();
        this.render();
        this.showNotification(`Cleared ${completedCount} completed task(s)!`);
    }

    toggleSort() {
        this.isSorted = !this.isSorted;
        this.render();
        this.showNotification(this.isSorted ? 'Sorted by priority' : 'Sorting disabled');
    }

    getFilteredTodos() {
        let filtered = this.todos.filter(todo => {
            const categoryMatch = this.filterCategory === 'all' || todo.category === this.filterCategory;
            const priorityMatch = this.filterPriority === 'all' || todo.priority === this.filterPriority;
            return categoryMatch && priorityMatch;
        });

        if (this.isSorted) {
            const priorityOrder = { high: 0, medium: 1, low: 2 };
            filtered.sort((a, b) => {
                if (a.completed !== b.completed) {
                    return a.completed ? 1 : -1;
                }
                return priorityOrder[a.priority] - priorityOrder[b.priority];
            });
        }

        return filtered;
    }

    getStats() {
        const total = this.todos.length;
        const completed = this.todos.filter(t => t.completed).length;
        const pending = total - completed;
        return { total, completed, pending };
    }

    updateStats() {
        const stats = this.getStats();
        document.getElementById('totalCount').textContent = stats.total;
        document.getElementById('completedCount').textContent = stats.completed;
        document.getElementById('pendingCount').textContent = stats.pending;
    }

    render() {
        const todoList = document.getElementById('todoList');
        const emptyState = document.getElementById('emptyState');
        const filteredTodos = this.getFilteredTodos();

        todoList.innerHTML = '';

        if (filteredTodos.length === 0) {
            emptyState.classList.add('show');
        } else {
            emptyState.classList.remove('show');
            filteredTodos.forEach(todo => {
                todoList.appendChild(this.createTodoElement(todo));
            });
        }

        this.updateStats();
    }

    createTodoElement(todo) {
        const item = document.createElement('div');
        item.className = `todo-item ${todo.priority}-priority ${todo.completed ? 'completed' : ''}`;
        item.dataset.id = todo.id;

        const createdDate = new Date(todo.createdAt).toLocaleDateString('en-US', {
            month: 'short',
            day: 'numeric'
        });

        item.innerHTML = `
            <div class="checkbox ${todo.completed ? 'checked' : ''}" onclick="app.toggleTodo(${todo.id})">
                ${todo.completed ? '✓' : ''}
            </div>
            <div class="todo-content">
                <div class="todo-text">${this.escapeHtml(todo.text)}</div>
                <div class="todo-meta">
                    <span class="todo-category">${todo.category}</span>
                    <span class="todo-priority ${todo.priority}">${todo.priority}</span>
                    <span class="todo-date">${createdDate}</span>
                </div>
            </div>
            <button class="delete-btn" onclick="app.deleteTodo(${todo.id})">×</button>
        `;

        return item;
    }

    escapeHtml(text) {
        const map = {
            '&': '&amp;',
            '<': '&lt;',
            '>': '&gt;',
            '"': '&quot;',
            "'": '&#039;'
        };
        return text.replace(/[&<>"']/g, m => map[m]);
    }

    exportData() {
        const dataStr = JSON.stringify(this.todos, null, 2);
        const dataBlob = new Blob([dataStr], { type: 'application/json' });
        const url = URL.createObjectURL(dataBlob);
        const link = document.createElement('a');
        link.href = url;
        link.download = `todos_${new Date().toISOString().split('T')[0]}.json`;
        link.click();
        URL.revokeObjectURL(url);
        this.showNotification('Data exported successfully!');
    }

    saveToStorage() {
        try {
            localStorage.setItem(this.storageKey, JSON.stringify(this.todos));
        } catch (error) {
            console.error('Error saving to localStorage:', error);
            this.showNotification('Error saving data!');
        }
    }

    loadFromStorage() {
        try {
            const stored = localStorage.getItem(this.storageKey);
            this.todos = stored ? JSON.parse(stored) : [];
        } catch (error) {
            console.error('Error loading from localStorage:', error);
            this.todos = [];
        }
    }

    showNotification(message) {
        // Create a simple notification (you can enhance this)
        const notification = document.createElement('div');
        notification.style.cssText = `
            position: fixed;
            bottom: 20px;
            right: 20px;
            background: #10b981;
            color: white;
            padding: 12px 20px;
            border-radius: 8px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
            animation: slideUp 0.3s ease-out;
            z-index: 1000;
            font-weight: 500;
        `;
        notification.textContent = message;
        document.body.appendChild(notification);

        setTimeout(() => {
            notification.style.animation = 'slideDown 0.3s ease-out';
            setTimeout(() => notification.remove(), 300);
        }, 3000);
    }
}

// Initialize app
let app;
document.addEventListener('DOMContentLoaded', () => {
    app = new TodoApp();
});

// Add animation keyframes
const style = document.createElement('style');
style.textContent = `
    @keyframes slideDown {
        from {
            opacity: 1;
            transform: translateY(0);
        }
        to {
            opacity: 0;
            transform: translateY(20px);
        }
    }
`;
document.head.appendChild(style);
