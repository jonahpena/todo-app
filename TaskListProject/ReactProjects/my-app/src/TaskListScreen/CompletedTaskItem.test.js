import { render, screen } from "@testing-library/react";
import CompletedTaskItem from "./CompletedTaskItem";

test("renders CompletedListItem component", () => {
  const item = {
    id: 1,
    title: "Sample completed task",
  };

  render(
    <CompletedTaskItem
      item={item}
      completedItems={[]}
      handleMoveBackToTask={() => {}}
      handleRemoveItem={() => {}}
    />
  );

  expect(screen.getByText(/Sample completed task/i)).toBeInTheDocument();
});

// Add more tests to cover the component's functionality
