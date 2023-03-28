import React from "react";
import "./TaskList.css";
import CompletedListItem from "./CompletedListItem";

function CompletedTaskListItems({
  completedItems,
  showCompletedItems,
  setShowCompletedItems,
  handleRemoveItem,
  handleMoveBackToTask,
}) {
  const hasCompletedItems = completedItems.length > 0;

  return (
    hasCompletedItems && (
      <div className="completed-items-dropdown">
        <button
          className="complete-button waves-effect waves-light btn"
          onClick={() => setShowCompletedItems(!showCompletedItems)}
        >
          Completed
        </button>
        {showCompletedItems && (
          <ul data-testid="completed-task-list">
            {[...completedItems].map((item, index) => (
              <CompletedListItem
                key={index}
                item={item}
                handleRemoveItem={handleRemoveItem}
                handleMoveBackToTask={handleMoveBackToTask}
                data-testid="completed-task-list-item"
              />
            ))}
          </ul>
        )}
      </div>
    )
  );
}

export default CompletedTaskListItems;
