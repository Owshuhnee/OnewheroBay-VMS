// Analytics.js - Complete file

document.addEventListener('DOMContentLoaded', function () {
    console.log('Analytics page loaded');
    loadStats();
    loadRecentBookings();
    loadPopularEvents();
});

async function loadStats() {
    try {
        const response = await fetch('/api/analytics/stats');
        if (!response.ok) throw new Error('Failed to fetch stats');

        const data = await response.json();
        console.log('Stats:', data);

        document.getElementById('total-visitors').textContent = data.totalVisitors;
        document.getElementById('active-bookings').textContent = data.activeBookings;
        document.getElementById('month-bookings').textContent = data.thisMonthBookings;
        document.getElementById('revenue').textContent = '$' + data.revenue.toFixed(2);
        document.getElementById('upcoming-events').textContent = data.upcomingEvents;
        document.getElementById('completed-events').textContent = data.completedEvents;
    } catch (error) {
        console.error('Error loading stats:', error);
    }
}

async function loadRecentBookings() {
    try {
        const response = await fetch('/api/analytics/recent-bookings');
        if (!response.ok) throw new Error('Failed to fetch bookings');

        const bookings = await response.json();
        console.log('Bookings:', bookings);

        const list = document.getElementById('recent-bookings-list');
        list.innerHTML = '';

        if (bookings.length === 0) {
            list.innerHTML = '<p>No recent bookings</p>';
            return;
        }

        bookings.forEach(booking => {
            const item = document.createElement('div');
            item.className = 'booking-item';
            item.innerHTML = `
                <div><strong>${booking.name}</strong> - ${booking.eventName}</div>
                <div>${booking.date} - ${booking.status}</div>
            `;
            list.appendChild(item);
        });
    } catch (error) {
        console.error('Error loading bookings:', error);
    }
}

async function loadPopularEvents() {
    try {
        const response = await fetch('/api/analytics/popular-events');
        if (!response.ok) throw new Error('Failed to fetch events');

        const events = await response.json();
        console.log('Events:', events);

        const list = document.getElementById('popular-events-list');
        list.innerHTML = '';

        if (events.length === 0) {
            list.innerHTML = '<p>No events data</p>';
            return;
        }

        const maxBookings = Math.max(...events.map(e => e.bookings));

        events.forEach(event => {
            const item = document.createElement('div');
            item.className = 'event-item';
            const percentage = (event.bookings / maxBookings) * 100;

            item.innerHTML = `
                <div>${event.name}</div>
                <div class="progress-bar">
                    <div class="progress-fill" style="width: ${percentage}%; background: #315a35; height: 20px;"></div>
                </div>
                <div>${event.bookings} bookings</div>
            `;
            list.appendChild(item);
        });
    } catch (error) {
        console.error('Error loading events:', error);
    }
}