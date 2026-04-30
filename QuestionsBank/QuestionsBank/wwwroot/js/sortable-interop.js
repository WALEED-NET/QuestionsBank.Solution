// Sortable.js wrapper for Blazor
window.sortableInterop = {
    initSortable: function (elementId, dotNetHelper, callbackMethod, includeParentId = false) {
        const el = document.getElementById(elementId);
        if (!el) {
            console.warn('Element not found:', elementId);
            return;
        }

        // Destroy existing instance if any
        if (el.sortableInstance) {
            el.sortableInstance.destroy();
        }

        el.sortableInstance = new Sortable(el, {
            animation: 150,
            handle: '.drag-handle',
            ghostClass: 'sortable-ghost',
            chosenClass: 'sortable-chosen',
            dragClass: 'sortable-drag',
            onEnd: function (evt) {
                const newOrder = Array.from(el.children).map((row, index) => {
                    const item = {
                        id: row.dataset.id,
                        order: index + 1
                    };
                    // Include parent ID if needed (for content/questions under topic)
                    if (includeParentId && row.dataset.topicId) {
                        item.topicId = row.dataset.topicId;
                    }
                    return item;
                });
                
                if (dotNetHelper && callbackMethod) {
                    dotNetHelper.invokeMethodAsync(callbackMethod, JSON.stringify(newOrder));
                }
            }
        });
    },

    destroySortable: function (elementId) {
        const el = document.getElementById(elementId);
        if (el && el.sortableInstance) {
            el.sortableInstance.destroy();
            el.sortableInstance = null;
        }
    },
    
    // Initialize all sortables for expanded topic details
    initTopicDetailsSortables: function (topicId, dotNetHelper) {
        // Content sortable
        const contentEl = document.getElementById('content-sortable-' + topicId);
        if (contentEl) {
            this.initSortable('content-sortable-' + topicId, dotNetHelper, 'OnContentReordered', true);
        }
        
        // Questions sortable
        const questionsEl = document.getElementById('topic-questions-sortable-' + topicId);
        if (questionsEl) {
            this.initSortable('topic-questions-sortable-' + topicId, dotNetHelper, 'OnTopicQuestionsReordered', true);
        }
    }
};
