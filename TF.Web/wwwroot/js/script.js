document.addEventListener('DOMContentLoaded', function () {
    const tasks = document.querySelectorAll('.task');

    let draggedTask = null;

    tasks.forEach(task => {
        task.addEventListener('dragstart', function () {
            draggedTask = this;
            this.setAttribute('data-dragging', 'true');
            setTimeout(() => {
                this.style.display = 'none';
            }, 0);
        });

        task.addEventListener('dragend', function () {
            draggedTask = null;
            this.removeAttribute('data-dragging');
            setTimeout(() => {
                this.style.display = '';
            }, 0);
        });
    });

    const taskList = document.querySelector('.task-list');

    taskList.addEventListener('dragover', function (e) {
        e.preventDefault();
        const afterElement = getDragAfterElement(taskList, e.clientY);
        const draggable = document.querySelector('[data-dragging]');
        if (afterElement == null) {
            taskList.appendChild(draggable);
        } else {
            taskList.insertBefore(draggable, afterElement);
        }
    });

    function getDragAfterElement(container, y) {
        const draggableElements = [...container.querySelectorAll('.task:not([data-dragging])')];

        return draggableElements.reduce((closest, child) => {
            const box = child.getBoundingClientRect();
            const offset = y - box.top - box.height / 2;
            if (offset < 0 && offset > closest.offset) {
                return { offset: offset, element: child };
            } else {
                return closest;
            }
        }, { offset: Number.NEGATIVE_INFINITY }).element;
    }
});
