if (typeof window !== 'undefined' && window.document) {

    // Fetch data from the API and update the DOM
    function fetchOverallCost() {
        return fetch('https://localhost:7180/api/Order/OverallCost')
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                //console.log(response.json());
                return response.json();
            })
            .catch(error => {
                console.error('Error fetching overall cost:', error);
            });
    }

    // Fetch number of orders from API
    function fetchOrderCount() {
        return fetch('https://localhost:7180/api/Order/Count')
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }else {
                    return response.json();
                }
            })
            .catch(error => {
                console.error('Error fetching order count:', error);
            });
    }

    function fetchMaxCostOrder() {
        return fetch('https://localhost:7180/api/Order/MaxCostOrder')
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .catch(error => {
                console.error('Error fetching max cost order:', error);
            });
    }

    // Calculate average cost per order
    function calculateAverageCost(totalSpent, orderCount) {
        if (orderCount === 0) {
            return 0; // Return 0 if there are no orders to avoid division by zero
        }
        return totalSpent / orderCount;
    }

    function updateOverallCost(overallCost) {
        const totalSpentElement = document.getElementById('totalSpent');
        if (totalSpentElement) {
            totalSpentElement.textContent = `Overall money spent: €${overallCost}`;
        } else {
            console.error('Element with id "totalSpent" not found');
        }
    }

    function updateOrderCount(orderCount){
        const orderCountElement = document.getElementById('orderCount');
        if (orderCountElement) {
            orderCountElement.textContent = `Number of Orders: ${orderCount}`;
        } else {
            console.error('Element with id "orderCount" not found');
        }
    }

    function updateAverage(averageCost) {
        const averageCostElement = document.getElementById('averageSpent');
        if (averageCostElement) {
            averageCostElement.textContent = `Average money spent per Order: €${averageCost}`;
        }else {
            console.error('Element with id "averageSpent" not found');
        }
    }

    function updateMaxCostOrder(maxCostOrder) {
        const orderDate = document.getElementById('topOrderDate');
        const orderConsumers = document.getElementById('topOrderConsumerCount');
        const orderCost = document.getElementById('topOrderCost');
        const orderVendor = document.getElementById('topOrderVendor');
        if (orderDate && orderConsumers && orderCost && orderVendor) {
            orderDate.textContent = `On ${maxCostOrder.createdAt},`;
            orderDate.textContent = orderDate.textContent.slice(0, -15)+',';
            orderConsumers.textContent = `${getNumberOfConsumers(maxCostOrder)} people spent`;
            orderCost.textContent = `€${maxCostOrder.cost} at`;
            orderVendor.textContent = `${maxCostOrder.vendor.name}`;
        }
    }

    // Function to get the number of consumers from the order object
    function getNumberOfConsumers(order) {
        if (order && order.consumers) {
            return order.consumers.length;
        } else {
            return 0;
        }
    }

    function checkAPIConnection(){
        return fetch('https://localhost:7180/api/Order')
            .then(response => {
                return response.ok; // Return true if response is ok, otherwise false
            })
            .catch(error => {
                return false; // Return false if there is an error
            });
    }

    function fetchDataAndUpdateDOM(){
        checkAPIConnection().then(isReachable => {
            if (isReachable) {
                let total = 0;
                let count = 0;

                // Fetch overall cost
                fetchOverallCost().then(overallCost => {
                    total = overallCost;
                    updateOverallCost(overallCost);

                    // After fetching overall cost, fetch order count
                    fetchOrderCount().then(orderCount => {
                        count = orderCount;
                        updateOrderCount(orderCount);

                        // After fetching order count, calculate and update average
                        const avg = calculateAverageCost(total, count);
                        updateAverage(avg.toFixed(2));
                    });
                });

                // Fetch max cost order
                fetchMaxCostOrder().then(order => updateMaxCostOrder(order));
            } else {
                console.log('API is not reachable');
            }
        });
    }

    // Periodically check if API is reachable and fetch data every 10 seconds
    setInterval(() => {
        fetchDataAndUpdateDOM();
    }, 10000);

    document.addEventListener('DOMContentLoaded', () => {
        fetchDataAndUpdateDOM();
    });
}
