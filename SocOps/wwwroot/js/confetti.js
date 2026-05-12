window.confetti = (function () {
    let running = false;
    function start() {
        if (running) return;
        running = true;
        const duration = 2000;
        const end = Date.now() + duration;

        const colors = ['#ef4444', '#f97316', '#f59e0b', '#10b981', '#3b82f6', '#8b5cf6'];
        const canvas = document.createElement('canvas');
        canvas.style.position = 'fixed';
        canvas.style.left = '0';
        canvas.style.top = '0';
        canvas.style.width = '100%';
        canvas.style.height = '100%';
        canvas.style.pointerEvents = 'none';
        canvas.width = window.innerWidth;
        canvas.height = window.innerHeight;
        document.body.appendChild(canvas);
        const ctx = canvas.getContext('2d');

        const particles = [];
        for (let i = 0; i < 80; i++) {
            particles.push({
                x: Math.random() * canvas.width,
                y: -10 - Math.random() * 200,
                vx: (Math.random() - 0.5) * 6,
                vy: Math.random() * 4 + 2,
                size: Math.random() * 8 + 4,
                color: colors[Math.floor(Math.random() * colors.length)],
                rot: Math.random() * Math.PI
            });
        }

        function frame() {
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            const now = Date.now();
            particles.forEach(p => {
                p.x += p.vx;
                p.y += p.vy;
                p.vy += 0.05;
                p.rot += 0.1;
                ctx.save();
                ctx.translate(p.x, p.y);
                ctx.rotate(p.rot);
                ctx.fillStyle = p.color;
                ctx.fillRect(-p.size / 2, -p.size / 2, p.size, p.size);
                ctx.restore();
            });
            if (Date.now() < end) {
                requestAnimationFrame(frame);
            } else {
                document.body.removeChild(canvas);
                running = false;
            }
        }

        requestAnimationFrame(frame);
    }

    return { start };
})();
