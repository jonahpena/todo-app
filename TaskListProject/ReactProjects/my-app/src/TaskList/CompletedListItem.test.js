import { render, screen } from "@testing-library/react";
import CompletedListItem from "./CompletedListItem";

test("renders CompletedListItem component", () => {
  const item = {
    id: 1,
    title: "Sample completed task",
  };

  render(
    <CompletedListItem
      item={item}
      completedItems={[]}
      handleMoveBackToTask={() => {}}
      handleRemoveItem={() => {}}
    />
  );

  expect(screen.getByText(/Sample completed task/i)).toBeInTheDocument();
});

// Add more tests to cover the component's functionality
